namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type TextpathSettings()  =
    inherit CommandSettings()

type TextpathExample() =
    inherit Command<TextpathSettings>()
    interface ICommandLimiter<TextpathSettings>

    override _.Execute(_context, _) =
        0

type TextpathDocumentation() =
    inherit Command<TextpathSettings>()
    interface ICommandLimiter<TextpathSettings>

    override _.Execute(_context, _) =
        Cli.Theme.setDocumentationStyle
        Edgy "Sorry, this documentation is not available yet." |> toConsole    
        0
