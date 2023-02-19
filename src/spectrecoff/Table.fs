[<AutoOpen>]
module SpectreCoff.Table

open Spectre.Console
open SpectreCoff.Layout
open SpectreCoff.Output

[<AutoOpen>]
module Column = 
    type ColumnLayout =
        { Alignment: Alignment
          LeftPadding: int
          RightPadding: int
          Wrap: bool }
    
    let defaultColumnLayout: ColumnLayout =
        { Alignment = Center
          LeftPadding = 2
          RightPadding = 2
          Wrap = true }

    type ColumnDefinition = 
        { Header: OutputPayload
          Footer: Option<OutputPayload>
          Layout: Option<ColumnLayout> }

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

    let private addFooter footerOption (column: TableColumn) =
        match footerOption with
        | Some footer -> column.Footer <- payloadToRenderable footer
        | None -> ignore()
        column

    let toSpectreColumn (definition: ColumnDefinition) =
        toSpectreContentColumn definition.Header 
        |> applyLayout definition.Layout
        |> addFooter definition.Footer 
    
    let column header =
        { Header = header; Footer = None; Layout = Some defaultColumnLayout }

    let withLayout layout column = 
        { column with Layout = Some layout }

    let withLayouts layouts columns = 
        columns
        |> List.zip layouts 
        |> List.map (fun (column, layout) -> { column with Layout = Some layout })

    let withSameLayout layout columns =
        columns |> List.map (fun column -> { column with Layout = Some layout })

    let withFooter footer column = 
        { column with Footer = Some footer }

    let withFooters footers columns = 
        columns
        |> List.zip footers 
        |> List.map (fun (column, footer) -> { column with Footer = Some footer })

[<AutoOpen>]
module Row = 
    type Row =
        | Payloads of OutputPayload list
        | Strings of string list
        | Numbers of int list

    let private getValues (row: Row) =
        match row with
        | Payloads payloads -> payloads |> List.map payloadToRenderable
        | Strings values -> values |> List.map (fun value -> Text value)
        | Numbers values -> values |> List.map (fun value -> Text (value.ToString()))
        |> List.toArray

    let addRowToTable (table: Table) (row: Row) =
        let values = getValues row
        table.AddRow(values) |> ignore

    let addRowToGrid (grid: Grid) (row: Row) =
        let values = getValues row
        grid.AddRow(values) |> ignore

type TableLayout =
    {  Border: TableBorder;
       Sizing: SizingBehaviour;
       HideHeaders: bool;
       HideFooters: bool;
       Alignment: Alignment }

let defaultTableLayout: TableLayout =
    {  Border = TableBorder.Rounded
       Sizing = Expand
       Alignment = Left
       HideHeaders = false 
       HideFooters = false }

let toOutputPayload table =
    table
    :> Rendering.IRenderable
    |> Renderable

let customTable (layout: TableLayout) (columnDefinitions: ColumnDefinition list) (rows: Row list) =
    let table = Table()

    match layout.Alignment with
    | Left -> table.LeftAligned() |> ignore
    | Right -> table.RightAligned() |> ignore
    | Center -> table.Centered() |> ignore

    match layout.Sizing with
    | Expand -> table.Expand <- true
    | Collapse -> table.Collapse() |> ignore
    table.Border <- layout.Border

    table.ShowHeaders <- not layout.HideHeaders
    table.ShowFooters <- not layout.HideFooters

    columnDefinitions 
    |> List.map toSpectreColumn
    |> List.iter (fun column -> table.AddColumn column |> ignore)
 
    rows |> List.iter (addRowToTable table)
    table

let withCaption caption (table: Table) =
    table.Caption <- TableTitle (caption, Style (calmColor, System.Nullable(), System.Nullable()))
    table

let withTitle title (table: Table) =
    table.Title <- TableTitle (title, Style (pumpedColor, System.Nullable(), System.Nullable()))
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
    |> List.iter (addRowToGrid grid)
    grid

type Table with
    member self.toOutputPayload = toOutputPayload self
    member self.addRow = addRowToTable self

type Grid with
    member self.toOutputPayload = toOutputPayload self
    member self.addRow = addRowToGrid self
