[<AutoOpen>]
module SpectreCoff.Prompt

open Spectre.Console

type PromptOptions =
    { Secret: bool
      Optional: bool }

type MultiSelectionPromptOptions =
    { PageSize: int }

type GroupedSelectionPromptOptions =
    { PageSize: int
      Optional: bool
      SelectionMode: SelectionMode }

type ChoiceGroup<'T> =
    { Group: 'T
      Choices: 'T array }

type ChoiceGroups<'T> =
    { Groups: ChoiceGroup<'T> list
      DisplayFunction: 'T -> string }

let mutable defaultChoiceGroups =
    { Groups = []
      DisplayFunction = id }

let mutable defaultOptions =
    { Secret = false
      Optional = false }

let mutable defaultMultiSelectionOptions =
    { PageSize = 10 }

let mutable defaultGroupedSelectionOptions =
    { PageSize = 10
      Optional = false
      SelectionMode = SelectionMode.Leaf }

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

    let groupedMultiSelectionPrompt<'T> options question (choiceGroups: ChoiceGroups<'T>) =
        choiceGroups.Groups
        |> Seq.fold (fun (prompt: MultiSelectionPrompt<'T>) group -> prompt.AddChoiceGroup<'T>(group.Group, group.Choices)) (MultiSelectionPrompt())
        |> fun prompt ->
            prompt.Title <- question
            prompt.PageSize <- options.PageSize
            prompt.Converter <- choiceGroups.DisplayFunction
            prompt.Required <- not options.Optional
            prompt.Mode <- options.SelectionMode
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

let chooseFrom (choices: string list) question =
    prompt (Prompts.selectionPrompt question choices)

let chooseMultipleFromWith options (choices: string list) question =
    prompt (Prompts.multiSelectionPrompt question choices options)

let chooseMultipleFrom =
    chooseMultipleFromWith defaultMultiSelectionOptions

let chooseGroupedFromWith<'T> options (groupedChoices: ChoiceGroups<'T>) question =
    prompt (Prompts.groupedMultiSelectionPrompt options question groupedChoices) |> List.ofSeq

let chooseGroupedFrom<'T> =
    chooseGroupedFromWith<'T> defaultGroupedSelectionOptions

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