namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff.BarChart
open SpectreCoff.Cli
open SpectreCoff.Layout
open SpectreCoff.Rule
open SpectreCoff.Output
open SpectreCoff

type DemoSettings() =
    inherit CommandSettings()

type DemoExample() =
    inherit Command<DemoSettings>()
    interface ICommandLimiter<DemoSettings>

    override _.Execute(_context, _settings) =

        Theme.setDocumentationStyle
        let fruits = [
            ChartItem ("Apple", 12)
            ChartItem ("Orange", 3)
            ChartItem ("Banana", 6)
            ChartItem ("Strawberry", 15)
        ]
        alignment <- Left
        
        
                NewLine |> toConsole

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
