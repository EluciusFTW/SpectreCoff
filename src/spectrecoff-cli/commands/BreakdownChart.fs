namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open Spectre.Console
open SpectreCoff.Cli
open SpectreCoff.Output
open SpectreCoff.Rule
open SpectreCoff.Layout
open SpectreCoff.Chart
open SpectreCoff.Chart.BreakdownChart

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
            NL
            E "My life as a developer :("
            NL
            breakdownChart items
        ] |> toConsole
        0

type BreakdownChartDocumentation() =
    inherit Command<BreakdownChartSettings>()
    interface ICommandLimiter<BreakdownChartSettings>

    override _.Execute(_context, _settings) =
        Theme.setDocumentationStyle
        Many [
            NL
            pumped "Breakdown Chart" |> alignedRule Left
            NL
            Edgy "Under construction...."
            NL
        ] |> toConsole
        0
