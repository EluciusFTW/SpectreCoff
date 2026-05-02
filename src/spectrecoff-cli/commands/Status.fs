namespace SpectreCoff.Cli.Commands

open System
open System.Threading.Tasks
open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type StatusSettings()  =
    inherit CommandSettings()

type StatusExample() =
    inherit Command<StatusSettings>()
    interface ICommandLimiter<StatusSettings>

    override _.Execute(_context, _settings) =
        let normalThinkingSpinner: CustomSpinner =
            { Message = "Thinking"
              Spinner = Some Spinner.Known.Pong
              Look = Some { calmLook with Color = Some Color.Green } }

        let harderThinkingSpinner =
           { normalThinkingSpinner with
               Message = "Thinking harder..."
               Look = Some { calmLook with Color = Some Color.DarkOrange } }

        let maximumThinkingSpinner =
            {
                Message = "Maximum thinking!!!"
                Look = Some { calmLook with Color = Some Color.Red }
                Spinner = Some Spinner.Known.Balloon2 }

        let asyncProcess (context: StatusContext) =
            task {
                do! Task.Delay 500

                updateWithCustomSpinner harderThinkingSpinner context |> ignore

                do! Task.Delay 500
                updateWithCustomSpinner maximumThinkingSpinner context |> ignore

                do! Task.Delay 200
                return "42"
            }
        "Press any key to start" |> C |> toConsole
        Console.ReadLine () |> ignore
        Status.start "Meaning of Life" asyncProcess
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> P
        |> toConsole
        0
