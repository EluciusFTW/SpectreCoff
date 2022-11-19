﻿namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff
open SpectreCoff.BarChart
open SpectreCoff.Layout
open SpectreCoff.Output

type ProgressSettings() =
    inherit CommandSettings()

type Progress() =
    inherit Command<ProgressSettings>()
    interface ICommandLimiter<ProgressSettings>

    override _.Execute(_context, _settings) =
        let items = [
            ChartItem ("Rule", 10)
            ChartItem ("Figlet", 10)
            ChartItem ("Output", 8)
            ChartItem ("Panel", 8)
            ChartItem ("Table", 6)
            ChartItem ("Prompt", 5)
            ChartItem ("Chart", 4)
            ChartItemWithColor ("Tree", 0, Color.Red)
            ChartItemWithColor ("Rows", 0, Color.Red)
            ChartItemWithColor ("Calendar", 0, Color.Red)
            ChartItemWithColor ("Grid", 0, Color.Red)
            ChartItemWithColor ("Padder", 0, Color.Red)
            ChartItemWithColor ("Canvas", 0, Color.Red)
            ChartItemWithColor ("CanvasImage", 0, Color.Red)
            ChartItemWithColor ("TextPath", 0, Color.Red)
        ]
        alignment <- Alignment.Left

        ManyMarkedUp [
            NewLine
            C "Below, a breakdown of our progress porting Spectre.Console modules to SpectreCoff:"
            NewLine
        ] |> toConsole

        items
        |> barChart "SpectreCoff Module Progress"
        |> BarChart.toConsole
        0