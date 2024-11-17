namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff

type PromptSettings()  =
    inherit CommandSettings()

type Food =
    { Name : string
      Tastiness : int option
      Healthiness: int option }

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
            "I'll ask again, but this time I'll suggest 16, it's your secret :)"
            |> askWithSuggesting<int> { defaultOptions with Secret = true } 16

        if (amount = amountAgain)
            then "You didn't flinch, huh?"
            else "I see you changed your mind ..."
        |> printMarkedUp

        let answer = confirm "Do you want to eat them right away?"
        match answer with
        | true -> Pumped "Bon apetit!"
        | false -> Edgy "Ok, maybe later :/"
        |> toConsole

        let stronglyTypedFoods =
            { DisplayFunction = (fun food -> food.Name)
              Groups =
                [ { Group = { Name = "Fruits"; Healthiness = None; Tastiness = None }; Choices = [| { Name = "Apple"; Healthiness = Some 4; Tastiness = Some 5 } |] }
                  { Group = { Name = "Berries"; Healthiness = None; Tastiness = None }; Choices = [| { Name = "Blueberry"; Healthiness = Some 4; Tastiness = Some 5 }; { Name = "Strawberry"; Healthiness = Some 8; Tastiness = Some 2 } |] } ]}

        let stronglyTypedResult =
            chooseGroupedFromWith defaultMultiSelectionOptions stronglyTypedFoods "Choose to measure the tastiness and healthiness of your foods"
        let healthiness = stronglyTypedResult |> List.fold (fun acc food -> acc + food.Healthiness.Value) 0
        let tastiness = stronglyTypedResult |> List.fold (fun acc food -> acc + food.Tastiness.Value) 0

        P $"The combined healthiness of your foods is {healthiness} and the combined tastiness is {tastiness}" |> toConsole

        let stringlyTypedFoods = { defaultChoiceGroups with Groups = [ { Group = "Fuits"; Choices = [| "Apple"; "Banana"; "Orange" |] }; { Group = "Berries"; Choices = [| "Blueberry"; "Strawberry" |] } ] }

        let stringlyTypedResult = chooseGroupedFrom stringlyTypedFoods "Choose a combination of fruits and berries"

        P $"Aha, so combined you like {stringlyTypedResult.Length} kinds of fruits and berries" |> toConsole
        0
