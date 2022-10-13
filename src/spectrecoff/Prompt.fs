module SpectreFs.Prompt
open Spectre.Console 

let selectionPrompt question choices = 
    let prompt = SelectionPrompt()
    prompt.AddChoices (choices |> Seq.toArray) |> ignore
    prompt.Title <- question
    prompt

let prompt prompter =
    AnsiConsole.Prompt prompter;

let promptChoices title choices = 
    prompt (selectionPrompt title choices)

let confirm message = 
    AnsiConsole.Confirm message