namespace SpectreFs.Sample.Commands

open Spectre.Console.Cli
open SpectreFs.Prompt
open SpectreFs.Output

type PromptExampleSettings()  =
    inherit CommandSettings()

type PromptExample() =
    inherit Command<TableExampleSettings>()
    interface ICommandLimiter<TableExampleSettings>

    override _.Execute(_context, _) = 
        let chosenFruit = promptChoices "Which shall it be?" ["Kiwi"; "Pear"; "Grape"]
        printMarkedUp $"Excellent choice, a {emphasize chosenFruit}!"

        let answer = confirm "Do you want to eat it right away?"
        match answer with
        | true -> printMarkedUp (emphasize "Bon apetit!")
        | false -> printMarkedUp (warn "Ok, maybe later :/")
        0