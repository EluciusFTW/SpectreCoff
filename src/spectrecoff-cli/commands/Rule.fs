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
        $"""{emphasize "Hello"}""" 
            |> alignedRule Left   
            |> SpectreCoff.Rule.toConsole

        "Fellow" 
            |> rule
            |> SpectreCoff.Rule.toConsole
         
        $"""{warn "Developer"}""" 
            |> alignedRule Right 
            |> SpectreCoff.Rule.toConsole

        emptyRule |> SpectreCoff.Rule.toConsole
        0

type RuleDocumentation() =
    inherit Command<RuleSettings>()
    interface ICommandLimiter<RuleSettings>

    override _.Execute(_context, _settings) =
        
        Theme.setDocumentationStyle
        NewLine |> toConsole
        alignedRule Left (emphasize "Rule module") |> SpectreCoff.Rule.toConsole
        
        ManyMarkedUp [
            CO [S "This module provides functionality from the "; E "rule widget"; S " of Spectre.Console"]
            NewLine
            S "The rule can be used by the rule function:"
            BI [ 
                E "rule: string -> Rule"
            ]
            NL
            CO [S "This rule will use the "; E "Rule.defaultAlignment"; S ", which is set to "; E "Center"; S " but can be modified."]
            NL
            S "Other rules can be used without changing the default by passing in the alignment as an argument to: "
            BI [ 
                E "alignedRule: Alignment -> string -> Rule"
            ]
            NL
            CO [S "The rule can be printed to the console with the "; E "toConsole"; S " function."]
            NL
        ] |> toConsole
        0