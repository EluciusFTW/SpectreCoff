namespace SpectreCoff.Cli.Commands

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

        let thinkingProcess (context: StatusContext) =
            task {
                do! Task.Delay(3000)
                let harderThinkingSpinner =
                    { normalThinkingSpinner with Message = "Thinking harder..."; Look = Some { calmLook with Color = Some Color.DarkOrange } }
                updateWithCustomSpinner harderThinkingSpinner context |> ignore

                do! Task.Delay(3000)
                let maximumThinkingSpinner =
                    { normalThinkingSpinner with Message = "Maximum thinking!!!"; Look = Some { calmLook with Color = Some Color.Red }; Spinner = Some Spinner.Known.Balloon2 }
                updateWithCustomSpinner maximumThinkingSpinner context |> ignore

                do! Task.Delay(3000)
            }

        (startWithCustomSpinner normalThinkingSpinner thinkingProcess).Wait()
        P "No result" |> toConsole
        0

type StatusDocumentation() =
    inherit Command<StatusSettings>()
    interface ICommandLimiter<StatusSettings>

    override _.Execute(_context, _settings) =
        Cli.Theme.setDocumentationStyle

        BL |> toConsole
        pumped "Status module"
        |> alignedRule Left
        |> toConsole

        Many [
            C "This module provides functionality from the status widget of Spectre.Console ("
            Link "https://spectreconsole.net/widgets/status"
            C ")"
        ] |> toConsole
        0