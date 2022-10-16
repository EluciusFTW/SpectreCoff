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
    | Payloads of PrintPayload list
    | Renderables of Rendering.IRenderable list
    | Strings of string list
    | Numbers of int list

type HeaderContent =
    | Simple of string
    | Renderable of Rendering.IRenderable
    | Payload of PrintPayload

type Header = 
    | DefaultHeader of HeaderContent
    | CustomHeader of HeaderContent * ColumnLayout

let private applyLayout (layout: ColumnLayout) (column : TableColumn) =
    match layout.Alignment with
    | Left -> column.LeftAligned() |> ignore
    | Right -> column.RightAligned() |> ignore
    | Center -> column.Centered() |> ignore

    column.PadLeft layout.LeftPadding |> ignore
    column.PadRight layout.RightPadding |> ignore

    column.NoWrap <- not layout.Wrap
    column

let private toSpectreContentColumn (content: HeaderContent) =
    match content with
    | Simple value -> TableColumn(value) 
    | Renderable renderable -> TableColumn(renderable) 
    | Payload renderable -> TableColumn(renderable |> toRenderablePayload)

let private toSpectreColumn (header: Header) =
    match header with
    | DefaultHeader content -> toSpectreContentColumn content |> applyLayout defaultColumnLayout
    | CustomHeader (content, layout) -> toSpectreContentColumn content |> applyLayout layout

let addRow (table: Table) (row: Row) =
    let values = 
        match row with
        | Renderables rs -> rs
        | Strings values -> values |> List.map (fun value -> Text value)
        | Numbers values -> values |> List.map (fun value -> Text (value.ToString()))
        | Payloads payloads -> payloads |> List.map (fun payload -> toRenderablePayload payload)

    table.AddRow(values) |> ignore

let customTable (layout: TableLayout) (headers: Header list) (rows: Row list) =
    let table = new Table()

    match layout.Alignment with
    | Left -> table.LeftAligned() |> ignore
    | Right -> table.RightAligned() |> ignore
    | Center -> table.Centered() |> ignore

    match layout.Sizing with
    | Expand -> table.Expand <- true
    | Collapse -> table.Collapse() |> ignore

    match layout.HideHeaders with
    | true -> table.HideHeaders() |> ignore
    | false -> () |> ignore
    
    headers |> List.iter (
        fun header -> 
            toSpectreColumn header 
            |> table.AddColumn 
            |> ignore)
    rows |> List.iter (addRow table)
    table

let table =
    customTable defaultTableLayout


// Output methods
let toConsole (table: Table) = 
    table |> AnsiConsole.Write

let printcr columns rows = 
    customTable defaultTableLayout rows columns |> toConsole
