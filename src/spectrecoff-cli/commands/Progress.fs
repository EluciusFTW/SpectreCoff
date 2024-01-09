namespace SpectreCoff.Cli.Commands

open System.Threading.Tasks
open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type ProgressSettings() =
    inherit CommandSettings()

type ProgressExample() =
    inherit Command<ProgressSettings>()
    interface ICommandLimiter<ProgressSettings>

    override _.Execute(_context, _settings) =
        let operation (context: ProgressContext) =
            task {
                let task1 =
                    "Turtle"
                    |> HotPercentageTask
                    |> realizeIn context
                let task2 =
                    (60.0, "Rabbit")
                    |> ColdCustomTask
                    |> realizeIn context
                while not context.IsFinished do
                    task1 |> incrementBy 5 |> ignore
                    if task1.Value > 50 then
                        startTask task2
                    if task2.IsStarted then
                        task2 |> incrementBy 7 |> ignore
                    do! Task.Delay(300)
            }
        let template =
            emptyTemplate
            |> withDescriptionColumn
            |> withSpinnerColumn
            |> withRemainingTimeColumn
            |> withProgressBarColumn
        (operation |> startCustom template).Wait()
        0

open SpectreCoff.Cli.Documentation

type ProgressDocumentation() =
    inherit Command<ProgressSettings>()
    interface ICommandLimiter<ProgressSettings>

    override _.Execute(_context, _settings) =
        setDocumentationStyle
        Many [
            spectreDocSynopsis "Progress module" "This module provides functionality from the progress widget of Spectre.Console" "widgets/progress"
            C "A progress can be started by calling the"; P "start"; C"or"; P "startCustom"; C "functions:"
            funcsOutput [{ Name = "start"; Signature = "ProgressOperation -> Task<unit>" }; { Name = "startCustom"; Signature = "ProgressTemplate -> ProgressOperation -> Task<unit>" }]

            C "The"; P "ProgressTemplate"; C "record can be used to further define the behavior of the progress widget:"
            propsOutput [
                { Name = "AutoClear"; Type = (nameof bool); Explanation = "defines if the console should be cleared before progress starts" }
                { Name = "AutoRefresh"; Type = (nameof bool); Explanation = "defines if the progress should be refreshed automatically" }
                { Name = "HideCompleted"; Type = (nameof bool); Explanation = "defines if finished tasks should be hidden" }
                { Name = "Columns"; Type = $"{(nameof ProgressColumn)} list"; Explanation = "defines the columns of the progress widget" }
            ]

            C "For convenience the"; P "defaultTemplate"; C "and the"; P "emptyTemplate"; C "variables can be used and customized using these functions:"
            funcsOutput [
                { Name = "withDescriptionColumn"; Signature = "ProgressTemplate -> ProgressTemplate" }
                { Name = "withSpinnerColumn"; Signature = "ProgressTemplate -> ProgressTemplate" };
                { Name = "withProgressBarColumn"; Signature = "ProgressTemplate -> ProgressTemplate" }
                { Name = "withPercentageColumn"; Signature = "ProgressTemplate -> ProgressTemplate" };
                { Name = "withRemainingTimeColumn"; Signature = "ProgressTemplate -> ProgressTemplate";  }]

            C "Inside the passed ProgressOperation, tasks can be added to the"; P "ProgressContext"; C "using the"; P "realizeIn"; C "function:"
            funcsOutput [{ Name = "realizeIn"; Signature = "ProgressContext -> TaskTemplate -> ProgressTask" }]

            C "The"; P "TaskTemplate"; C "type is a DU with several cases that represent different kind of tasks:"
            discriminatedUnionOutput [
                { Label = "HotPercentageTask"; Args = ["string"]; Explanation = "A task that starts automatically. Completion is represented by a value between 0 and 100" }
                { Label = "ColdPercentageTask"; Args = ["string"]; Explanation = "A task that has to be started manually. Completion is represented by a value between 0 and 100" }
                { Label = "HotCustomTask"; Args = ["float * string"]; Explanation = "A task that starts automatically. Completion is represented by a value between 0 and the contained max value" }
                { Label = "ColdCustomTask"; Args = ["float * string"]; Explanation = "A task that has to be started manually. Completion is represented by a value between 0 and the contained max value" }
            ]

            C "Finally, inside the operation a task can be incremented using the"; P "incrementBy"; C "function:"
            funcsOutput [{ Name = "incrementBy"; Signature = "int -> ProgressTask -> ProgressTask" }]
        ] |> toConsole
        0