﻿module SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff.BarChart
open SpectreCoff.Cli

type BarChartSettings() =
    inherit CommandSettings()

type BarChartExample() =
    inherit Command<BarChartSettings>()
    interface ICommandLimiter<BarChartSettings>

    override _.Execute(_context, _settings) =
        // todo add progress of each feature as meta example here and put screenshot in the readme
        let items = [
            ChartItem ("Apple", 12)
            ChartItem ("Orange", 3)
            ChartItem ("Banana", 6)
            ChartItem ("Kiwi", 6)
            ChartItem ("Strawberry", 15)
            ChartItem ("Mango", 16)
            ChartItem ("Peach", 6)
        ]

        items |> barChart |> toConsole
        0

type BarChartDocumentation() =
    inherit Command<BarChartSettings>()
    interface ICommandLimiter<BarChartSettings>

    override _.Execute(_context, _settings) =
        Theme.setDocumentationStyle
        0