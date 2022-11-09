namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff.BarChart
open SpectreCoff.Cli
open SpectreCoff.Layout
open SpectreCoff.Rule
open SpectreCoff.Output
open SpectreCoff

type BarChartSettings() =
    inherit CommandSettings()

type BarChartExample() =
    inherit Command<BarChartSettings>()
    interface ICommandLimiter<BarChartSettings>

    override _.Execute(_context, _settings) =
        // todo add progress of each feature as meta example here and put screenshot in the readme
        let fruits = [
            ChartItem ("Apple", 12)
            ChartItem ("Orange", 3)
            ChartItem ("Banana", 6)
            ChartItem ("Kiwi", 6)
            ChartItem ("Strawberry", 15)
            ChartItem ("Mango", 16)
            ChartItem ("Peach", 6)
            ChartItemWithColor ("White", 2, Color.White)
        ]
        alignment <- Left
        Theme.setDocumentationStyle
        
        "Inventory"
        |> alignedRule Left
        |> Rule.toConsole

        Many [
           "This is a summary of all the fruits in the storage."
           "Please consider when choosing what to eat!" 
        ] |> toConsole

        fruits
        |> barChart "Fruits"
        |> BarChart.toConsole

        NewLine |> toConsole
        Edgy "Don't let them spoil!" |> toConsole

        0

type BarChartDocumentation() =
    inherit Command<BarChartSettings>()
    interface ICommandLimiter<BarChartSettings>

    override _.Execute(_context, _settings) =
        Theme.setDocumentationStyle
        0
