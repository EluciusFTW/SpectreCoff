namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff.Layout
open SpectreCoff.Rule
open SpectreCoff.Output
open SpectreCoff.Cli

type RuleSettings()  =
    inherit CommandSettings()

type RuleExample() =
    inherit Command<RuleSettings>()
    interface ICommandLimiter<RuleSettings>

    override _.Execute(_context, _settings) =
        alignedRule Left  $"""{emphasize "Hello"}"""
        rule "Fellow"
        alignedRule Right $"""{warn "Developer"}"""
        emptyRule
        0

type RuleDocumentation() =
    inherit Command<RuleSettings>()
    interface ICommandLimiter<RuleSettings>

    override _.Execute(_context, _settings) =
        Theme.setDocumentationStyle
        
        printfn ""
        alignedRule Left (emphasize "Rule module")
        ManyMarkedUp [
            CO [S "This module provides functionality from the "; E "rule widget"; S " of Spectre.Console"]
            NewLine
            S "The rule can be used by the rule function:"
            BI [ 
                E "rule: string -> unit"
            ]
            C $"""{standard "This rule will use the default style, which can be changed by modifying "}{emphasize "Rule.defaultAlignment"}."""
            S "It is set to 'Center' by default. Other rules can be used without changing the default by passing in the alignment as an argument to: "
            BI [ 
                E "alignedRule: Alignment -> string -> unit"
            ]
        ] |> toConsole
        0