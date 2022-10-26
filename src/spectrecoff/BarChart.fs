module SpectreCoff.BarChart

open Spectre.Console
open SpectreCoff.Layout

let mutable width = 60
let mutable alignment = Alignment.Center

let mutable colors = [
    Color.Blue
    Color.Green
    Color.Red
    Color.Yellow
    Color.Aqua
    Color.Lime
]

type ChartItem = string * float

let createBasicChart label =
    let chart = BarChart()
    chart.Width <- width
    chart.Label <- label
    chart.LabelAlignment <- match alignment with
                            | Left -> Justify.Left
                            | Center -> Justify.Center
                            | Right -> Justify.Right
    chart

// todo each list item can also be a "custom item", containing also the color
let barChart label (items: List<ChartItem>) =
    let chart = createBasicChart label
    items
    |> List.indexed
    |> List.map (fun item -> BarChartItem(fst (snd item), snd (snd item), colors[fst item % colors.Length]))
    |> List.iter (fun item -> chart.AddItem(item) |> ignore)

    chart

let toConsole (barChart: BarChart) =
    barChart |> AnsiConsole.Write
