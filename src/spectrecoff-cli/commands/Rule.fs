namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff

type RuleSettings()  =
    inherit CommandSettings()

type RuleExample() =
    inherit Command<RuleSettings>()
    interface ICommandLimiter<RuleSettings>

    override _.Execute(_context, _settings) =
        pumped "Hello"
        |> alignedRule Left
        |> toConsole

        "Fellow"
        |> rule
        |> toConsole

        edgy "Developer"
        |> alignedRule Right
        |> toConsole

        emptyRule |> toConsole
        0
