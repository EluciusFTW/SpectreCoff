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
        let thinkingProcess (context: StatusContext) =
            task {
                do! Task.Delay(5000)
                context.Status <- "More thinking"
                context.SpinnerStyle <- { calmLook with Color = Some Color.Green } |> toSpectreStyle
                context.Spinner <- Spinner.Known.Balloon2
                do! Task.Delay(5000)
            }

        (startWithCustomSpinner "Thinking" Spinner.Known.Pong { calmLook with Color = Some Color.DarkOrange } thinkingProcess).Wait()
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