[<AutoOpen>]
module SpectreCoff.Status

open System.Threading.Tasks
open Spectre.Console

type CustomSpinner =
    { Message: string
      Spinner: Spinner Option
      Look: Look Option }

let start statusText (operation: StatusContext -> Task<unit>) =
    task { return! AnsiConsole.Status().StartAsync(statusText, operation) }

let startWithCustomSpinner spinner (operation: StatusContext -> Task<unit>) =
    task {
        let status = AnsiConsole.Status()

        status.Spinner <-
            match spinner.Spinner with
            | Some spinner -> spinner
            | None -> status.Spinner

        status.SpinnerStyle <-
            match spinner.Look with
            | Some look -> look |> toSpectreStyle
            | None -> status.SpinnerStyle

        return! status.StartAsync(spinner.Message, operation)
    }

let update newMessage (context: StatusContext) =
    context.Status <- newMessage
    context

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
