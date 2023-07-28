namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type CanvasSettings() =
    inherit CommandSettings()

type CanvasExample() =
    inherit Command<CanvasSettings>()
    interface ICommandLimiter<CanvasSettings>

    override _.Execute(_context, _settings) =
        0

type CanvasDocumentation() =
    inherit Command<CanvasSettings>()
    interface ICommandLimiter<CanvasSettings>

    override _.Execute(_context, _settings) =
        Cli.Theme.setDocumentationStyle
        EL |> toConsole
        
        pumped "Canvas module"
        |> alignedRule Left
        |> toConsole
        0
