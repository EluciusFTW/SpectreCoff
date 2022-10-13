namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff.Prompt
open SpectreCoff.Output

type PromptSettings()  =
    inherit CommandSettings()

type PromptExample() =
    inherit Command<PromptSettings>()
    interface ICommandLimiter<PromptSettings>

    override _.Execute(_context, _) = 
        let chosenFruit = promptChoices "Which shall it be?" ["Kiwi"; "Pear"; "Grape"]
        printMarkedUp $"Excellent choice, a {emphasize chosenFruit}!"

        let answer = confirm "Do you want to eat it right away?"
        match answer with
        | true -> printMarkedUp (emphasize "Bon apetit!")
        | false -> printMarkedUp (warn "Ok, maybe later :/")
        0

open SpectreCoff.Rule
open SpectreCoff.Cli

type PromptDocumentation() =
    inherit Command<PromptSettings>()
    interface ICommandLimiter<PromptSettings>
    
    override _.Execute(_context, _) = 

        Theme.setDocumentationStyle
        
        printfn ""
        alignedRule Left (emphasize "Prompt module")
        ManyMarkedUp [
            C $"""This module provides functionality from the {emphasize "text prompt"} module of Spectre.Console"""
            NewLine
            S "Currently, we expose two basic functionalities:"
            BI [ 
                CO [S "Confirmation: "; E "confirm = (message: string) -> bool"]
                CO [S "Choices: "; E "promptChoices = (choices: string list) -> string"]
            ]
            S "More to come soon!"
        ] |> toConsole
        0
