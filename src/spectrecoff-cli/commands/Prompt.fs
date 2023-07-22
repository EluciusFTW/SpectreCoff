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
                else $"Why didn't you pick {chosenFruit} in the first place?"
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

        EL |> toConsole
        pumped "Prompt module"
        |> alignedRule Left
        |> toConsole

        Many [
            Many [
                C "This module provides functionality from the prompts of Spectre.Console ("
                Link "https://spectreconsole.net/prompts"
                C ")"
            ]
            Many [C "This module provides functionality from the"; E "prompt"; C "of Spectre.Console"]
            EL
            emptyRule
            C "For prompting an answer from the user, the following functions can be used:"
            BI [
                P "ask<'T> = (question: string) -> 'T"
                P "askWith<'T> = (options: PromptOptions) (question: string) -> 'T"
                P "askSuggesting<'T> = (answer: 'T) (question: string) -> 'T"
                P "askWithSuggesting<'T> = (options: PromptOptions) (answer: 'T) (question: string) -> 'T"
            ]
            Many [C "Here,"; P "PromptOptions"; C "is a record with two boolean properties:"]
            BI [
               Many [P "Secret"; C "describes whether the characters are shown (default: false)"]
               Many [P "Optional"; C "describes whether empty is a valid input (default: false)"]
            ]
            EL
            emptyRule
            C "If the set of choices is finite, one of the following can be used:"
            BI [
                P "chooseFrom = (choices: string list) (question: string) -> string"
                P "chooseMultipleFrom = (choices: string list) (question: string) -> string list"
            ]
            EL
            emptyRule
            C "And finally, this function is suitable for a yes/no question:"
            BI [ P "confirm = (question: string) -> bool" ]
        ] |> toConsole
        0
