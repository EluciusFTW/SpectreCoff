namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff.Prompt
open SpectreCoff.Rule
open SpectreCoff.Output

type PromptSettings()  =
    inherit CommandSettings()

type PromptExample() =
    inherit Command<PromptSettings>()
    interface ICommandLimiter<PromptSettings>

    override _.Execute(_context, _) = 
        let chosenFruit = promptChoices "Which shall it be?" ["Kiwi"; "Pear"; "Grape"]
        printMarkedUpInline $"Excellent choice, a {emphasize chosenFruit}!"

        let answer = confirm "Do you want to eat it right away?"
        match answer with
        | true -> printMarkedUpInline (emphasize "Bon apetit!")
        | false -> printMarkedUpInline (warn "Ok, maybe later :/")
        0


open SpectreCoff.Layout
open SpectreCoff.Cli

type PromptDocumentation() =
    inherit Command<PromptSettings>()
    interface ICommandLimiter<PromptSettings>
    
    override _.Execute(_context, _) = 

        Theme.setDocumentationStyle
        NewLine |> toConsole
        alignedRule Left (emphasize "Prompt module") |> SpectreCoff.Rule.toConsole
        
        ManyMarkedUp [
            CO [
                S "This module provides functionality from the prompts of Spectre.Console ("
                Link "https://spectreconsole.net/prompts"
                S ")"
            ]
            CO [S "This module provides functionality from the "; E "prompt"; S " of Spectre.Console"]
            NL
            S "Currently, we expose two basic functionalities:"
            BI [ 
                E "confirm = (message: string) -> bool"
                E "promptChoices = (choices: string list) -> string"
            ]
            S "More to come soon!"
        ] |> toConsole
        0
