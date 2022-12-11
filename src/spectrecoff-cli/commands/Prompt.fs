namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff

type PromptSettings()  =
    inherit CommandSettings()

type PromptExample() =
    inherit Command<PromptSettings>()
    interface ICommandLimiter<PromptSettings>

    override _.Execute(_context, _) = 

        let fruits = ["Kiwi"; "Pear"; "Grape"; "Plum"; "Banana" ; "Orange"; "Durian"]
        let chosenFruit = "If you had to pick one, which would it be?" |> chooseFrom fruits
        let chosenFruits = "Which all do you actually like?" |> chooseMultipleFrom fruits

        match chosenFruits.Count with
        | 0 -> "You don't like any fruit??"
        | 1 -> 
            if (chosenFruit = chosenFruits.ToArray()[0])
                then "Makes sense" 
                else $"Why didn't you pick {chosenFruit} in hte first place?"
        | _ -> $"Must be nice to like {chosenFruits.Count} different fruit!"
        |> printMarkedUp
        
        let amount = 
            $"How many {chosenFruit} do you want?" 
            |> ask<int> 
        
        let amountAgain = 
            $"I'll ask again, but this time I'll suggest 16, it's your secret :)" 
            |> askWithSuggesting<int> { defaultOptions with Secret = true } 16

        if (amount = amountAgain)
            then "You didn't flinch, huh?" 
            else $"I see you changed your mind ..."
        |> printMarkedUp

        let answer = confirm $"Do you want to eat them right away?"
        match answer with
        | true -> Pumped "Bon apetit!"
        | false -> Edgy "Ok, maybe later :/" 
        |> toConsole
        0

type PromptDocumentation() =
    inherit Command<PromptSettings>()
    interface ICommandLimiter<PromptSettings>
    
    override _.Execute(_context, _) = 
        Cli.Theme.setDocumentationStyle
        
        NewLine |> toConsole
        pumped "Prompt module"
        |> alignedRule Left 
        |> toConsole
        
        Many [
            CO [
                C "This module provides functionality from the prompts of Spectre.Console ("
                Link "https://spectreconsole.net/prompts"
                C ")"
            ]
            CO [C "This module provides functionality from the"; E "prompt"; C "of Spectre.Console"]
            NL
            C "Currently, we expose three basic functionalities:"
            BI [ 
                P "ask<'T> = (question: string) -> 'T"
                P "confirm = (question: string) -> bool"
                P "chooseFrom = (choices: string list) (question: string) -> string"
            ]
            CO [C "The generic type argument of"; P "ask"; C "let's you specify the expected type. If the answer does not fit, it will be rejected."] 
        ] |> toConsole
        0
