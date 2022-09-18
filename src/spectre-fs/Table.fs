module SpectreFs.Table
open Spectre.Console 
    
let toRenderableRow (items: Rendering.IRenderable list) = 
    new TableRow(items)

let toStringRow values = 
    toRenderableRow (values |> List.map (fun v -> new Markup(v.ToString())))

let toColumn (value: string) = 
    new TableColumn(value)

let toColumns values =
    values |> List.map(fun v -> toColumn (v.ToString()))

let table (columns: TableColumn list) (rows: TableRow list) = 
    let t = new Table()
    columns |> List.iter (fun c -> t.AddColumn(c) |> ignore)
    rows |> List.iter (fun r -> t.AddRow(r) |> ignore)
    t

let stable columns rows = 
    table (toColumns columns) (rows |> List.map toStringRow) 

let print tab = AnsiConsole.Write (tab: Table)
let printcr columns rows = print (table rows columns)
