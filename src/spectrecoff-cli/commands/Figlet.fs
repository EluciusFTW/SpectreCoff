namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type FigletSettings() =
    inherit CommandSettings()

type FigletExample() =
    inherit Command<FigletSettings>()
    interface ICommandLimiter<FigletSettings>

    override _.Execute(_context, _settings, _) =
        "Star ..." |> customFiglet Left Color.SeaGreen1 |> toConsole

        "Wars!" |> figlet |> toConsole

        "Smushed"
        |> customFigletWithMode FigletLayoutMode.Smushed Left Color.SeaGreen1
        |> toConsole

        0
