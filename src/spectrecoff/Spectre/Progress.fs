[<AutoOpen>]
module SpectreCoff.Progress

open System.Threading.Tasks
open Spectre.Console

type TaskTemplate =
    | HotPercentageTask of string
    | ColdPercentageTask of string
    | HotCustomTask of float*string
    | ColdCustomTask of float*string

type ProgressTemplate =
    { AutoClear: bool
      AutoRefresh: bool
      HideCompleted: bool
      Columns: ProgressColumn list }

type ProgressOperation = ProgressContext -> Task<unit>

let withDescriptionColumn template =
    { template with Columns = template.Columns@[TaskDescriptionColumn()]  }

let withRemainingTimeColumn template =
    { template with Columns = template.Columns@[RemainingTimeColumn()] }

let withSpinnerColumn template =
    { template with Columns = template.Columns@[SpinnerColumn()] }

let withProgressBarColumn template =
    { template with Columns = template.Columns@[ProgressBarColumn()] }

let withPercentageColumn template =
    { template with Columns = template.Columns@[PercentageColumn()] }

let emptyTemplate =
    { AutoClear = true
      AutoRefresh = true
      HideCompleted = false
      Columns = [] }

let defaultTemplate =
    emptyTemplate
    |> withDescriptionColumn
    |> withProgressBarColumn
    |> withPercentageColumn



let startCustom template (operation: ProgressOperation) =
    task {
    return! AnsiConsole
        .Progress()
        .Columns(template.Columns |> List.toArray)
        .StartAsync(operation)
}

let start =
    startCustom defaultTemplate

let realizeIn (context: ProgressContext) task =
    match task with
    | HotPercentageTask description -> context.AddTask(description)
    | ColdPercentageTask description -> context.AddTask(description, false)
    | HotCustomTask (maxValue, description) -> context.AddTask(description, true, maxValue)
    | ColdCustomTask (maxValue, description) -> context.AddTask(description, false, maxValue)

let incrementBy value (task: ProgressTask) =
    task.Increment(value)
    task