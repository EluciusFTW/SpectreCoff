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
            C "The rule can be used by the rule function:"
            BI [
                P "rule: string -> OutputPayload"
            ]
            BL
            C "This rule will use the"; P "Rule.defaultAlignment,"; C "which is set to"; P "Center"; C "but can be modified."
            BL
            C "Other rules can be used without changing the default by passing in the alignment as an argument to: "
            BI [
                P "alignedRule: Alignment -> string -> OutputPayload"
            ]
            BL
            C "The rule can be printed to the console with the"; P "toConsole"; C "function."
            BL
        ] |> toConsole
        0