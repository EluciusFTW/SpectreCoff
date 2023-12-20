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

open SpectreCoff.Cli.Documentation

type BreakdownChartDocumentation() =
    inherit Command<BreakdownChartSettings>()
    interface ICommandLimiter<BreakdownChartSettings>

    override _.Execute(_context, _settings) =
        setDocumentationStyle
        Many [
            spectreDocSynopsis "BreakdownChart submodule" "This submodule provides functionality from the BreakdownChart widget of Spectre.Console" "widgets/breakdownchart"
            C "The breakdown chart can be used using the breakdownChart function:"
            funcsOutput [
                { Name = "breakdownChart"; Signature = "ChartItem list -> OutputPayload" }
            ]
            BL
            C "The"; P "ChartItem"; C "union type consists of two options:"
            discriminatedUnionOutput [
                { Label = "ChartItem"; Args = ["string"; "float"]; Explanation = "Consists of the label and a value for the item" }
                { Label = "ChartItemWithColor"; Args = ["string"; "float"; "Color"]; Explanation = "Consists of the label and a value for the item, as well as the color." }
            ]
            BL
            C "If no color is explicitly defined, the colors will cycle through a set of colors defined in the"; P "Colors"; C "variable."
            C "This variable can be overwritten with a custom set if the default one is not to your taste."
            BL
            C "Similarly, the:"; P "width"; C "variable which controls the width of the whole chart can be overwritten"
        ] |> toConsole
        0
