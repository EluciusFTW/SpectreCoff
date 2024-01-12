namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open Spectre.Console
open SpectreCoff.Output
open SpectreCoff.Chart

type BreakdownChartSettings() =
    inherit CommandSettings()

type BreakdownChartExample() =
    inherit Command<BreakdownChartSettings>()
    interface ICommandLimiter<BreakdownChartSettings>

    override _.Execute(_context, _settings) =
        let items = [
            ChartItem ("Refinements", 2)
            ChartItem ("Dailies", 2)
            ChartItem ("Retrospectives", 1)
            ChartItem ("Random meetings", 7)
            ChartItem ("Fixing bugs", 3)
            ChartItemWithColor ("Developing features", 1, Color.Red)
        ]

        Many [
            BL
            E "My life as a developer :("
            BL
            breakdownChart items
        ] |> toConsole
        0
