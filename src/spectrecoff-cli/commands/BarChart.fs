namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff.BarChart
open SpectreCoff.Cli
open SpectreCoff.Layout

type BarChartSettings() =
    inherit CommandSettings()

type BarChartExample() =
    inherit Command<BarChartSettings>()
    interface ICommandLimiter<BarChartSettings>

    override _.Execute(_context, _settings) =
        // todo add progress of each feature as meta example here and put screenshot in the readme
        let items = [
            UncoloredChartItem ("Apple", 12)
            UncoloredChartItem ("Orange", 3)
            UncoloredChartItem ("Banana", 6)
            UncoloredChartItem ("Kiwi", 6)
            UncoloredChartItem ("Strawberry", 15)
            UncoloredChartItem ("Mango", 16)
            UncoloredChartItem ("Peach", 6)
            ColoredChartItem ("White", 2, Color.White)
        ]
        alignment <- Alignment.Left

        items
        |> barChart "Fruits"
        |> toConsole
        0

type BarChartDocumentation() =
    inherit Command<BarChartSettings>()
    interface ICommandLimiter<BarChartSettings>

    override _.Execute(_context, _settings) =
        Theme.setDocumentationStyle
        0
