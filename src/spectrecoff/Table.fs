[<AutoOpen>]
module SpectreCoff.Table

open Spectre.Console
open SpectreCoff.Layout
open SpectreCoff.Output

type TableLayout =
    {  Border: TableBorder;
       Sizing: SizingBehaviour;
       HideHeaders: bool;
       Alignment: Alignment }

type ColumnLayout =
    {  Alignment: Alignment
       LeftPadding: int
       RightPadding: int
       Wrap: bool }

let defaultTableLayout: TableLayout =
    {  Border = TableBorder.Rounded
       Sizing = Expand
       Alignment = Left
       HideHeaders = false }

let defaultColumnLayout: ColumnLayout =
    {  Alignment = Center
       LeftPadding = 2
       RightPadding = 2
       Wrap = true }

type Row =
    | Payloads of OutputPayload list
    | Strings of string list
    | Numbers of int list

type ColumnDef = 
    { H: OutputPayload
      F: Option<OutputPayload>
      L: Option<ColumnLayout> }

let private applyDefiniteLayout (layout: ColumnLayout) (column: TableColumn) = 
    match layout.Alignment with
    | Left -> column.LeftAligned() |> ignore
    | Right -> column.RightAligned() |> ignore
    | Center -> column.Centered() |> ignore

    column.PadLeft layout.LeftPadding |> ignore
    column.PadRight layout.RightPadding |> ignore

    column.NoWrap <- not layout.Wrap
    column

let private applyLayout (possibleLayout: Option<ColumnLayout>) (column: TableColumn) =
    match possibleLayout with 
    | Some layout -> applyDefiniteLayout layout column
    | None -> column

let private toSpectreContentColumn (payload: OutputPayload) =
    TableColumn(payload |> payloadToRenderable)

let private toSpectreColumn (definition: ColumnDef) =
    toSpectreContentColumn definition.H |> applyLayout definition.L

let private getValues (row: Row) =
    match row with
    | Payloads payloads -> payloads |> List.map payloadToRenderable
    | Strings values -> values |> List.map (fun value -> Text value)
    | Numbers values -> values |> List.map (fun value -> Text (value.ToString()))
    |> List.toArray

let private addRowToTable (table: Table) (row: Row) =
    let values = getValues row
    table.AddRow(values) |> ignore

let private addRowToGrid (grid: Grid) (row: Row) =
    let values = getValues row
    grid.AddRow(values) |> ignore

let toLaidOutColumn layout header =
     { H = header; F = None; L = Some layout }

let toColumnDefinitions headers = 
    headers |> List.map (toLaidOutColumn defaultColumnLayout)

let toOutputPayload table =
    table
    :> Rendering.IRenderable
    |> Renderable

let customTable (layout: TableLayout) (columnDefinitions: ColumnDef list) (rows: Row list) =
    let table = Table()

    match layout.Alignment with
    | Left -> table.LeftAligned() |> ignore
    | Right -> table.RightAligned() |> ignore
    | Center -> table.Centered() |> ignore

    match layout.Sizing with
    | Expand -> table.Expand <- true
    | Collapse -> table.Collapse() |> ignore
    table.Border <- layout.Border

    match layout.HideHeaders with
    | true -> table.HideHeaders() |> ignore
    | false -> ()

    columnDefinitions 
    |> List.map toSpectreColumn
    |> List.iter (fun column -> table.AddColumn column |> ignore)
 
    rows |> List.iter (addRowToTable table)
    table

let table =
    customTable defaultTableLayout

let grid (rows: Row list) =
    let numberOfColumns =
        rows
        |> List.map (fun row ->
           match row with
           | Numbers numbers -> numbers.Length
           | Payloads payloads -> payloads.Length
           | Strings strings -> strings.Length)
        |> List.max

    let grid = Grid().AddColumns numberOfColumns
    rows
    |> List.map getValues
    |> List.map grid.AddRow
    |> ignore
    grid

type Table with
    member self.toOutputPayload = toOutputPayload self
    member self.addRow = addRowToTable self

type Grid with
    member self.toOutputPayload = toOutputPayload self
    member self.addRow = addRowToGrid self
