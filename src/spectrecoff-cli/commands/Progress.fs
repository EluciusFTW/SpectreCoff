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
                return "The race is over!"
            }
        let template =
            emptyTemplate
            |> withDescriptionColumn
            |> withSpinnerColumn
            |> withRemainingTimeColumn
            |> withProgressBarColumn
        (operation |> startCustom template).Result |> P |> toConsole
        0
