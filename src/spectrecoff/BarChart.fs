module SpectreCoff.BarChart

open Spectre.Console

let mutable width = 60

let mutable colors = [
    Color.Blue
    Color.Green
    Color.Red
    Color.Yellow
    Color.Aqua
    Color.Lime
]

type ChartItem = string * float

// todo each list item can also be a "custom item", containing also the color
// todo set label and allow setting alignment
let barChart (items: List<ChartItem>) =
    let chart = BarChart()
    chart.Width <- width

    items
    |> List.indexed
    |> List.map (fun item -> BarChartItem(fst (snd item), snd (snd item), colors[fst item % colors.Length]))
    |> List.iter (fun item -> chart.AddItem(item) |> ignore)

    chart

let toConsole (barChart: BarChart) =
    barChart |> AnsiConsole.Write
