[<AutoOpen>]
module SpectreCoff.Status

open Spectre.Console

type CustomSpinner =
    { Message: string
      Spinner: Spinner Option
      Look: Look Option }

type StatusOperation<'Result> = StatusContext -> Async<'Result>

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
    async {
        return!
            AnsiConsole
                .Status()
                .StartAsync(statusText, (fun context -> operation context |> Async.StartAsTask))
            |> Async.AwaitTask
    }

let startWithCustomSpinner<'Result> spinner (operation: StatusOperation<'Result>) =
    async {
        let status = AnsiConsole.Status() |> configureStatus spinner
        return!
            status.StartAsync(spinner.Message, fun context -> operation context |> Async.StartAsTask)
            |> Async.AwaitTask
    }

let update newMessage (context: StatusContext) =
    context.Status <- newMessage
    context
