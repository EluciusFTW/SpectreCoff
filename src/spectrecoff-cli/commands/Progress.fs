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
            ChartItem ("Json", 9)
            ChartItem ("Output", 9)
            ChartItem ("Prompt", 8)
            ChartItem ("Chart", 8)
            ChartItem ("Padder", 8)
            ChartItem ("Calendar", 6)
            ChartItem ("Tree", 6)
            ChartItemWithColor ("Canvas", 0, Color.Red)
            ChartItemWithColor ("Columns", 0, Color.Red)
            ChartItemWithColor ("Layout", 0, Color.Red)
            ChartItemWithColor ("Live Display", 0, Color.Red)
            ChartItemWithColor ("Progress", 0, Color.Red)
            ChartItemWithColor ("Status", 0, Color.Red)
        ]
        alignment <- Left

        Many [
            EL
            items |> barChart "SpectreCoff Module Progress"
        ] |> toConsole
        0
