[<AutoOpen>]
module SpectreCoff.LiveDisplay

open System.Threading.Tasks
open Spectre.Console

type LiveDisplayConfiguration =
    { AutoClear: bool
      Overflow: VerticalOverflow Option
      Cropping: VerticalOverflowCropping Option }

let defaultConfiguration =
    { AutoClear = false
      Overflow = None
      Cropping = None }

type LiveDisplayOperation = LiveDisplayContext -> Task<unit>

let start renderable (operation: LiveDisplayOperation) =
    task { return! AnsiConsole.Live(renderable).StartAsync(operation) }

let startWithCustomConfig config renderable (operation: LiveDisplayOperation) =
    task {
        let live = AnsiConsole.Live(renderable)
        live.AutoClear <- config.AutoClear
        live.Overflow <-
            match config.Overflow with
            | Some overflow -> overflow
            | None -> live.Overflow
        live.Cropping <-
            match config.Cropping with
            | Some cropping -> cropping
            | None -> live.Cropping
        return! live.StartAsync(operation)
    }