namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type ProgressSettings() =
    inherit CommandSettings()

type Progress() =
    inherit Command<ProgressSettings>()
    interface ICommandLimiter<ProgressSettings>

    override _.Execute(_context, _settings) =
        let items = [
            ChartItem ("Rule", 10)
            ChartItem ("Figlet", 10)
            ChartItem ("Panel", 10)
            ChartItem ("Table", 10)
            ChartItem ("TextPath", 10)
            ChartItem ("Grid", 10)
            ChartItem ("CanvasImage", 10)
            ChartItem ("Padder", 10)
            ChartItem ("Json", 9)
            ChartItem ("Output", 9)
            ChartItem ("Canvas", 8)
            ChartItem ("Prompt", 8)
            ChartItem ("Chart", 8)
            ChartItem ("Layout", 8)
            ChartItem ("Calendar", 6)
            ChartItem ("Tree", 6)
            ChartItemWithColor ("Columns", 0, Color.Red)
            ChartItemWithColor ("Live Display", 0, Color.Red)
            ChartItemWithColor ("Progress", 0, Color.Red)
            ChartItemWithColor ("Status", 0, Color.Red)
        ]
        alignment <- Left

        Many [
            BL
            items |> barChart "SpectreCoff Module Progress"
        ] |> toConsole
        0
