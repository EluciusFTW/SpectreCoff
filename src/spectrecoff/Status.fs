[<AutoOpen>]
module SpectreCoff.Status

open System.Threading.Tasks
open Spectre.Console

let start statusText (operation: StatusContext -> Task<unit>) =
    task { return! AnsiConsole.Status().StartAsync(statusText, operation) }

let startWithSpinner statusText spinner (operation: StatusContext -> Task<unit>) =
    task {
        let status = AnsiConsole.Status()
        status.Spinner <- spinner
        return! status.StartAsync(statusText, operation)
    }

let startWithCustomSpinner statusText spinner look (operation: StatusContext -> Task<unit>) =
    task {
        let status = AnsiConsole.Status()
        status.Spinner <- spinner
        status.SpinnerStyle <- look |> toSpectreStyle
        return! status.StartAsync(statusText, operation)
    }
