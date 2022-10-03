module SpectreFs.Table
open Spectre.Console

let toRenderableRow (items: Rendering.IRenderable list) =
    TableRow(items)

let toStringRow values =
    toRenderableRow (values |> List.map (fun v -> Markup(v.ToString())))

let toColumn (value: string) =
    TableColumn(value)

let toColumns values =
    values |> List.map(fun v -> toColumn (v.ToString()))

let table (columns: TableColumn list) (rows: TableRow list) =
    let table = new Table()
    columns |> List.iter (fun column -> table.AddColumn(column) |> ignore)
    rows |> List.iter (fun row -> table.AddRow(row) |> ignore)
    table

let stable columns rows =
    table (toColumns columns) (rows |> List.map toStringRow)

let print table = AnsiConsole.Write (table: Table)
let printcr columns rows = print (table rows columns)
