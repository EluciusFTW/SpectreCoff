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

type RuleDocumentation() =
    inherit Command<RuleSettings>()
    interface ICommandLimiter<RuleSettings>

    override _.Execute(_context, _settings) =
        Cli.Theme.setDocumentationStyle
        
        NewLine |> toConsole
        pumped "Rule module"
        |> alignedRule Left 
        |> toConsole
        
        Many [
            CO [
                C "This module provides functionality from the rule widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/rule"
                C ")"
            ]
            NewLine
            C "The rule can be used by the rule function:"
            BI [ 
                P "rule: string -> OutputPayload"
            ]
            NL
            CO [C "This rule will use the"; P "Rule.defaultAlignment,"; C "which is set to"; P "Center"; C "but can be modified."]
            NL
            C "Other rules can be used without changing the default by passing in the alignment as an argument to: "
            BI [ 
                P "alignedRule: Alignment -> string -> OutputPayload"
            ]
            NL
            CO [C "The rule can be printed to the console with the"; P "toConsole"; C "function."]
            NL
        ] |> toConsole
        0