namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type JsonSettings()  =
    inherit CommandSettings()

type JsonExample() =
    inherit Command<JsonSettings>()
    interface ICommandLimiter<JsonSettings>

    override _.Execute(_context, _settings) =
        0

type JsonDocumentation() =
    inherit Command<JsonSettings>()
    interface ICommandLimiter<JsonSettings>

    override _.Execute(_context, _settings) =
        Cli.Theme.setDocumentationStyle

        NewLine |> toConsole
        pumped "Json module"
        |> alignedRule Left
        |> toConsole

        Many [
            CO [
                C "This module provides functionality from the json widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/Json"
                C ")"
            ]
        ] |> toConsole
        0