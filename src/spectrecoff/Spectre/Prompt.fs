[<AutoOpen>]
module SpectreCoff.Prompt

open System
open System.Collections.Generic
open Spectre.Console

type PromptOptions = {
    Secret: bool
    Optional: bool
    EditableSuggestion: bool
    ClearOnFinish: bool
}

type MultiSelectionPromptOptions = { PageSize: int; Optional: bool }

type GroupedSelectionPromptOptions = {
    PageSize: int
    Optional: bool
    SelectionMode: SelectionMode
}

type ChoiceGroup<'T> = { Group: 'T; Choices: 'T array }

type ChoiceGroups<'T> = {
    Groups: ChoiceGroup<'T> list
    DisplayFunction: 'T -> string
}

let mutable defaultChoiceGroups = { Groups = []; DisplayFunction = id }

let mutable defaultOptions = {
    Secret = false
    Optional = false
    EditableSuggestion = false
    ClearOnFinish = false
}

let mutable defaultMultiSelectionOptions = { PageSize = 10; Optional = false }

let mutable defaultGroupedSelectionOptions = {
    PageSize = 10
    Optional = false
    SelectionMode = SelectionMode.Leaf
}

[<RequireQualifiedAccess>]
module private Prompts =
    let selectionPrompt question choices =
        let prompt = SelectionPrompt()
        prompt.AddChoices(choices |> Seq.toArray) |> ignore
        prompt.Title <- question
        prompt

    let multiSelectionPrompt question choices (options: MultiSelectionPromptOptions) =
        let prompt = MultiSelectionPrompt()
        prompt.AddChoices(choices |> Seq.toArray) |> ignore
        prompt.Title <- question
        prompt.PageSize <- options.PageSize
        prompt.Required <- not options.Optional
        prompt

    let groupedMultiSelectionPrompt<'T> options question (choiceGroups: ChoiceGroups<'T>) =
        choiceGroups.Groups
        |> Seq.fold
            (fun (prompt: MultiSelectionPrompt<'T>) group -> prompt.AddChoiceGroup<'T>(group.Group, group.Choices))
            (MultiSelectionPrompt())
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
        prompt.EditableDefaultValue <- options.EditableSuggestion
        prompt.ClearOnFinish <- options.ClearOnFinish
        prompt

    let textPromptWithDefault<'T> question (answer: 'T) (options: PromptOptions) =
        let prompt = textPrompt<'T> question options
        prompt.DefaultValue answer

    let selectionPromptWithDefault question choices suggestion =
        let prompt = selectionPrompt question choices
        prompt.DefaultValue <- suggestion
        prompt

    let multiSelectionPromptWithDefault question choices options suggestion =
        let prompt = multiSelectionPrompt question choices options
        prompt.DefaultValue <- suggestion
        prompt

    let cancellableSelectionPrompt question choices =
        let prompt = SelectionPrompt<string option>()
        prompt.AddChoices(choices |> Seq.map Some |> Seq.toArray) |> ignore
        prompt.Title <- question
        prompt.Converter <- Option.defaultValue ""
        prompt.CancelResult <- Func<string option>(fun () -> None)
        prompt

    let cancellableMultiSelectionPrompt question choices options =
        let prompt = multiSelectionPrompt question choices options
        prompt.CancelResult <- Func<List<string>>(fun () -> null)
        prompt

    let cancellableGroupedMultiSelectionPrompt<'T> options question choiceGroups =
        let prompt = groupedMultiSelectionPrompt<'T> options question choiceGroups
        prompt.CancelResult <- Func<List<'T>>(fun () -> null)
        prompt

let private prompt prompter =
    AnsiConsole.Prompt prompter

let chooseFrom (choices: string list) question =
    prompt (Prompts.selectionPrompt question choices)

let chooseMultipleFromWith options (choices: string list) question =
    prompt (Prompts.multiSelectionPrompt question choices options) |> List.ofSeq

let chooseMultipleFrom = chooseMultipleFromWith defaultMultiSelectionOptions

let chooseGroupedFromWith<'T> options (groupedChoices: ChoiceGroups<'T>) question =
    prompt (Prompts.groupedMultiSelectionPrompt options question groupedChoices)
    |> List.ofSeq

let chooseGroupedFrom<'T> = chooseGroupedFromWith<'T> defaultGroupedSelectionOptions

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

let private toCancellableList (chosen: List<'T>) =
    match chosen with
    | null -> None
    | choices -> choices |> List.ofSeq |> Some

let chooseFromOrCancel (choices: string list) question =
    prompt (Prompts.cancellableSelectionPrompt question choices)

let chooseFromSuggesting suggestion (choices: string list) question =
    prompt (Prompts.selectionPromptWithDefault question choices suggestion)

let chooseMultipleFromSuggestingWith options suggestion (choices: string list) question =
    prompt (Prompts.multiSelectionPromptWithDefault question choices options suggestion)
    |> List.ofSeq

let chooseMultipleFromSuggesting suggestion =
    chooseMultipleFromSuggestingWith defaultMultiSelectionOptions suggestion

let chooseMultipleFromOrCancelWith options (choices: string list) question =
    prompt (Prompts.cancellableMultiSelectionPrompt question choices options)
    |> toCancellableList

let chooseMultipleFromOrCancel = chooseMultipleFromOrCancelWith defaultMultiSelectionOptions

let chooseGroupedFromOrCancelWith<'T> options (groupedChoices: ChoiceGroups<'T>) question =
    prompt (Prompts.cancellableGroupedMultiSelectionPrompt options question groupedChoices)
    |> toCancellableList

let chooseGroupedFromOrCancel<'T> = chooseGroupedFromOrCancelWith<'T> defaultGroupedSelectionOptions
