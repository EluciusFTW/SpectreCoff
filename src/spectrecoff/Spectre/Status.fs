[<AutoOpen>]
module SpectreCoff.Status

open System.Threading.Tasks
open Spectre.Console

type CustomSpinner =
    { Message: string
      Spinner: Spinner Option
      Look: Look Option }

type StatusOperation<'Result> = StatusContext -> Task<'Result>

let private configureStatus spinner (status: Status) =
    status.Spinner <-
        match spinner.Spinner with
        | Some spinner -> spinner
        | None -> status.Spinner

    status.SpinnerStyle <-
        match spinner.Look with
        | Some look -> look |> toSpectreStyle
        | None -> status.SpinnerStyle

    status

let updateWithCustomSpinner spinner (context: StatusContext) =
    context.Status <- spinner.Message

    context.Spinner <-
        match spinner.Spinner with
        | Some spinner -> spinner
        | None -> context.Spinner

    context.SpinnerStyle <-
        match spinner.Look with
        | Some look -> look |> toSpectreStyle
        | None -> context.SpinnerStyle

    context

let start<'Result> statusText (operation: StatusOperation<'Result>) =
    AnsiConsole
        .Status()
        .StartAsync(statusText, operation)

let startWithCustomSpinner<'Result> spinner (operation: StatusOperation<'Result>) =
    AnsiConsole.Status()
    |> configureStatus spinner
    |> fun status -> status.StartAsync(spinner.Message, operation)

let update newMessage (context: StatusContext) =
    context.Status <- newMessage
    context
