namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type BarChartSettings() =
    inherit CommandSettings()

type BarChartExample() =
    inherit Command<BarChartSettings>()
    interface ICommandLimiter<BarChartSettings>

    override _.Execute(_context, _settings) =
        let items = [
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

        items
        |> barChart "Fruits"
        |> toConsole
        0

type BarChartDocumentation() =
    inherit Command<BarChartSettings>()
    interface ICommandLimiter<BarChartSettings>

    override _.Execute(_context, _settings) =
        Cli.Theme.setDocumentationStyle
        NewLine |> toConsole
        pumped "BarChart submodule"
        |> alignedRule Left
        |> toConsole

        Many [
            C "This submodule provides functionality from the BarChart widget of Spectre.Console ("
            Link "https://spectreconsole.net/widgets/barchart"
            C ")"
            NL
            C "The bar chart can be used using the barChart function:"
            BI [
                P "barChart: string -> ChartItem list -> OutputPayload"
            ]
            NL
            Many [C "The"; P "ChartItem"; C "union type consists of two options:"]
            BI [
                Many [P "ChartItem:"; C "Consists of the label and a value for the item."]
                Many [P "ChartItemWithColor:"; C "Additionally defines a color the item will be rendered in."]
            ]
            NL
            Many [C "If no color is explicitly defined, the colors will cycle through a set of colors defined in the"; P "Colors"; C "variable."]
            C "This variable can be overwritten with a custom set if the default one is not to your taste."
            NL
            C "Similarly, two other variables can be overwritten:"
            BI [
                Many [P "width:"; C "Controls the width of the whole chart."]
                Many [P "alignment:"; C "Controls the alignment of the chart's label."]
            ]
        ] |> toConsole
        0
