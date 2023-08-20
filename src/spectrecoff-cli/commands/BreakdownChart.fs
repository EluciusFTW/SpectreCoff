namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open Spectre.Console
open SpectreCoff.Cli
open SpectreCoff.Output
open SpectreCoff.Rule
open SpectreCoff.Styling
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

type BreakdownChartDocumentation() =
    inherit Command<BreakdownChartSettings>()
    interface ICommandLimiter<BreakdownChartSettings>

    override _.Execute(_context, _settings) =
        Theme.setDocumentationStyle
        BL |> toConsole
        pumped "BreakdownChart submodule"
        |> alignedRule Left
        |> toConsole

        Many [
            C "This submodule provides functionality from the BreakdownChart widget of Spectre.Console ("
            Link "https://spectreconsole.net/widgets/breakdownchart"
            C ")"
            BL
            C "The breakdown chart can be used using the breakdownChart function:"
            BI [
                P "breakdownChart: ChartItem list -> OutputPayload"
            ]
            BL
            Many [C "The"; P "ChartItem"; C "union type consists of two options:"]
            BI [
                Many [P "ChartItem:"; C "Consists of the label and a value for the item."]
                Many [P "ChartItemWithColor:"; C "Additionally defines a color the item will be rendered in."]
            ]
            BL
            Many [C "If no color is explicitly defined, the colors will cycle through a set of colors defined in the"; P "Colors"; C "variable."]
            C "This variable can be overwritten with a custom set if the default one is not to your taste."
            BL
            Many [C "Similarly, the:"; P "width"; C "variable which controls the width of the whole chart can be overwritten"]
        ] |> toConsole
        0
