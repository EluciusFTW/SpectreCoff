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
        printMarkedUpInline $"Excellent choice, a {pumped chosenFruit}!"

        let answer = confirm "Do you want to eat it right away?"
        match answer with
        | true -> printMarkedUpInline (pumped "Bon apetit!")
        | false -> printMarkedUpInline (edgy "Ok, maybe later :/")
        0

open SpectreCoff.Layout
open SpectreCoff.Cli

type PromptDocumentation() =
    inherit Command<PromptSettings>()
    interface ICommandLimiter<PromptSettings>
    
    override _.Execute(_context, _) = 
        Theme.setDocumentationStyle
        
        NewLine |> toConsole
        pumped "Prompt module"
        |> alignedRule Left 
        |> SpectreCoff.Rule.toConsole
        
        ManyMarkedUp [
            CO [
                C "This module provides functionality from the prompts of Spectre.Console ("
                Link "https://spectreconsole.net/prompts"
                C ")"
            ]
            CO [C "This module provides functionality from the "; E "prompt"; C " of Spectre.Console"]
            NL
            C "Currently, we expose two basic functionalities:"
            BI [ 
                P "confirm = (message: string) -> bool"
                P "promptChoices = (choices: string list) -> string"
            ]
            C "More to come soon!"
        ] |> toConsole
        0
