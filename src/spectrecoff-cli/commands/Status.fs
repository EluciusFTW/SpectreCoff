namespace SpectreCoff.Cli.Commands

open System
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
            async {
                do! Async.Sleep 500

                updateWithCustomSpinner harderThinkingSpinner context |> ignore

                do! Async.Sleep 500
                updateWithCustomSpinner maximumThinkingSpinner context |> ignore

                do! Async.Sleep 200
                return "42"
            }
        let f = (Status.start "Meaning of Life" asyncProcess)
        "Operation ready, press any key to start" |> C |> toConsole
        Console.ReadLine () |> ignore
        f
        |> Async.RunSynchronously
        |> P
        |> toConsole
        0
