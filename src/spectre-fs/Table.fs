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
    let t = Table()
    columns |> List.iter (fun c -> t.AddColumn(c) |> ignore)
    rows |> List.iter (fun r -> t.AddRow(r) |> ignore)
    t

let stable columns rows =
    table (toColumns columns) (rows |> List.map toStringRow)

let print tab = AnsiConsole.Write (tab: Table)
let printcr columns rows = print (table rows columns)
