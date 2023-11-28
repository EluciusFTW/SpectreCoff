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

open SpectreCoff.Cli.Documentation

type RuleDocumentation() =
    inherit Command<RuleSettings>()
    interface ICommandLimiter<RuleSettings>

    override _.Execute(_context, _settings) =
        setDocumentationStyle
        Many [
            docSynopsis "Rule module" "This module provides functionality from the rule widget of Spectre.Console" "widgets/rule"
            BL
            C "Rules can be created using one of these functions:"
            funcsOutput [
                { Name = "rule"; Signature = "string -> OutputPayload" }
                { Name = "alignedRule"; Signature = "Alignment -> string -> OutputPayload" }
            ]
            BL
            C "The former will use the"; P "Rule.defaultAlignment,"; C "which is set to"; P "Center"; C "but can be modified."
            C "Without changing the default, the latter function also accepts an alignment as an argument."
            BL
            C "The rule can be printed to the console with the"; P "toConsole"; C "function."
            BL
        ] |> toConsole
        0