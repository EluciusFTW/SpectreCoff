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

let startWithCustomSpinner customerSpinner (operation: StatusContext -> Task<unit>) =
    task {
        let status = AnsiConsole.Status()

        status.Spinner <-
            match customerSpinner.Spinner with
            | Some spinner -> spinner
            | None _ -> status.Spinner

        status.SpinnerStyle <-
            match customerSpinner.Look with
            | Some look -> look |> toSpectreStyle
            | None _ -> status.SpinnerStyle

        return! status.StartAsync(customerSpinner.Message, operation)
    }

let update newMessage (context: StatusContext) =
    context.Status <- newMessage
    context

let updateWithCustomSpinner customerSpinner (context: StatusContext) =
    context.Status <- customerSpinner.Message

    context.Spinner <-
        match customerSpinner.Spinner with
        | Some spinner -> spinner
        | None _ -> context.Spinner

    context.SpinnerStyle <-
        match customerSpinner.Look with
        | Some look -> look |> toSpectreStyle
        | None _ -> context.SpinnerStyle

    context.Spinner
