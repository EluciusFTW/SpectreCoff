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
    | Renderables of Rendering.IRenderable list
    | Strings of string list
    | Numbers of int list

type Header =
    | DefaultHeader of OutputPayload
    | CustomHeader of OutputPayload * ColumnLayout

let private applyLayout (layout: ColumnLayout) (column : TableColumn) =
    match layout.Alignment with
    | Left -> column.LeftAligned() |> ignore
    | Right -> column.RightAligned() |> ignore
    | Center -> column.Centered() |> ignore

    column.PadLeft layout.LeftPadding |> ignore
    column.PadRight layout.RightPadding |> ignore

    column.NoWrap <- not layout.Wrap
    column

let private toSpectreContentColumn (payload: OutputPayload) =
    TableColumn(payload |> payloadToRenderable)

let private toSpectreColumn (header: Header) =
    match header with
    | DefaultHeader content -> toSpectreContentColumn content |> applyLayout defaultColumnLayout
    | CustomHeader (content, layout) -> toSpectreContentColumn content |> applyLayout layout

let toOutputPayload table =
    table 
    :> Rendering.IRenderable 
    |> Renderable

let addRow (table: Table) (row: Row) =
    let values =
        match row with
        | Renderables renderables -> renderables
        | Strings values -> values |> List.map (fun value -> Text value)
        | Numbers values -> values |> List.map (fun value -> Text (value.ToString()))
        | Payloads payloads -> payloads |> List.map payloadToRenderable

    table.AddRow(values) |> ignore

let customTable (layout: TableLayout) (headers: Header list) (rows: Row list) =
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

    headers |> List.iter (
        fun header ->
            toSpectreColumn header
            |> table.AddColumn
            |> ignore)
    rows |> List.iter (addRow table)
    table 

let table =
    customTable defaultTableLayout

type Table with 
    member self.toOutputPayload = toOutputPayload self 
