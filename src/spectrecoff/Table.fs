module SpectreCoff.Table
open Spectre.Console

type SizingBehaviour =
    | Expand
    | Collapse

type Alignment =
    | Left
    | Center
    | Right

type TableLayout = 
    {
        Border: TableBorder;
        Sizing: SizingBehaviour;
        HideHeaders: bool;
        Alignment: Alignment
    }

let (defaultTableLayout: TableLayout) = 
    {
        Border = TableBorder.Rounded
        Sizing = Expand
        Alignment = Left
        HideHeaders = false
    }

let toRenderableRow (items: Rendering.IRenderable list) =
    TableRow(items)

let toStringRow values =
    toRenderableRow (values |> List.map (fun v -> Markup(v.ToString())))

let toColumn (value: string) =
    TableColumn(value)

let toColumns values =
    values |> List.map(fun v -> toColumn (v.ToString()))

let table (layout: TableLayout) (columns: TableColumn list) (rows: TableRow list) =
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
    
    columns |> List.iter (fun column -> table.AddColumn(column) |> ignore)
    rows |> List.iter (fun row -> table.AddRow(row) |> ignore)
    table

let stable columns rows =
    table defaultTableLayout (toColumns columns) (rows |> List.map toStringRow)

let print table = AnsiConsole.Write (table: Table)
let printcr columns rows = print (table defaultTableLayout rows columns)
