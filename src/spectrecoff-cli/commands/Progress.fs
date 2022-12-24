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
            ChartItem ("Output", 9)
            ChartItem ("Prompt", 8)
            ChartItem ("Chart", 8)
            ChartItemWithColor ("Tree", 6, Color.Red)
            ChartItemWithColor ("Calendar", 0, Color.Red)
            ChartItemWithColor ("Grid", 0, Color.Red)
            ChartItemWithColor ("Padder", 0, Color.Red)
            ChartItemWithColor ("Canvas", 0, Color.Red)
            ChartItemWithColor ("CanvasImage", 0, Color.Red)
            ChartItemWithColor ("TextPath", 0, Color.Red)
            ChartItemWithColor ("Live Display", 0, Color.Red)
            ChartItemWithColor ("Progress", 0, Color.Red)
            ChartItemWithColor ("Status", 0, Color.Red)
        ]
        alignment <- Left

        Many [
            NewLine
            items |> barChart "SpectreCoff Module Progress"
        ] |> toConsole
        0
