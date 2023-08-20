[<AutoOpen>]
module SpectreCoff.Prompt

open Spectre.Console 

type PromptOptions = 
    { Secret: bool
      Optional: bool
    }

type MultiSelectionPromptOptions = 
    { PageSize: int }

let defaultOptions = { Secret = false; Optional = false; }
let defaultMultiSelectionOptions = { PageSize = 10; }

[<RequireQualifiedAccess>]
module private Prompts = 
    let selectionPrompt question choices = 
        let prompt = SelectionPrompt()
        prompt.AddChoices (choices |> Seq.toArray) |> ignore
        prompt.Title <- question
        prompt

    let multiSelectionPrompt question choices (options: MultiSelectionPromptOptions) = 
        let prompt = MultiSelectionPrompt()
        prompt.AddChoices (choices |> Seq.toArray) |> ignore
        prompt.Title <- question
        prompt.PageSize <- options.PageSize
        prompt

    let textPrompt<'T> question (options: PromptOptions) = 
        let prompt = TextPrompt<'T> question
        prompt.IsSecret <- options.Secret
        prompt.AllowEmpty <- options.Optional
        prompt

    let textPromptWithDefault<'T> question (answer: 'T) (options: PromptOptions) =
        let prompt = textPrompt<'T> question options
        prompt.DefaultValue answer

let private prompt prompter =
    AnsiConsole.Prompt prompter;

let chooseFrom choices question = 
    prompt (Prompts.selectionPrompt question choices)

let chooseMultipleFromWith options choices question = 
    prompt (Prompts.multiSelectionPrompt question choices options)

let chooseMultipleFrom choices question = 
    chooseMultipleFromWith defaultMultiSelectionOptions

let ask<'T> question = 
    prompt (Prompts.textPrompt<'T> question defaultOptions)

let askWith<'T> options question = 
    prompt (Prompts.textPrompt<'T> question options)

let askSuggesting<'T> answer question = 
    prompt (Prompts.textPromptWithDefault<'T> question answer defaultOptions)

let askWithSuggesting<'T> options answer question = 
    prompt (Prompts.textPromptWithDefault<'T> question answer options)

let confirm question = 
    AnsiConsole.Confirm question