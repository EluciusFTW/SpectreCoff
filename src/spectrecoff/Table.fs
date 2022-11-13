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

type Header = 
    | Header of OutputPayload
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
    | Header content -> toSpectreContentColumn content |> applyLayout defaultColumnLayout
    | CustomHeader (content, layout) -> toSpectreContentColumn content |> applyLayout layout

let addRow (table: Table) (row: OutputPayload list) =
    table.AddRow(row |> List.map payloadToRenderable) |> ignore

let customTable (layout: TableLayout) (headers: Header list) (rows: OutputPayload list list) =
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
let toPayload (table: Table) = 
    table :> Rendering.IRenderable |> Ren
