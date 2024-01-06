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
                    { normalThinkingSpinner with
                        Message = "Thinking harder...";
                        Look = Some { calmLook with Color = Some Color.DarkOrange } }
                updateWithCustomSpinner harderThinkingSpinner context |> ignore

                do! Task.Delay(3000)
                let maximumThinkingSpinner =
                    {
                        Message = "Maximum thinking!!!";
                        Look = Some { calmLook with Color = Some Color.Red };
                        Spinner = Some Spinner.Known.Balloon2 }
                updateWithCustomSpinner maximumThinkingSpinner context |> ignore

                do! Task.Delay(3000)
            }

        (startWithCustomSpinner normalThinkingSpinner thinkingProcess).Wait()
        P "No result" |> toConsole
        0

open SpectreCoff.Cli.Documentation

type StatusDocumentation() =
    inherit Command<StatusSettings>()
    interface ICommandLimiter<StatusSettings>

    override _.Execute(_context, _settings) =
        setDocumentationStyle
        Many [
            spectreDocSynopsis "Status module" "This module provides functionality from the status widget of Spectre.Console" "widgets/status"

            C "A (long running) operation can be started with the status widget using the"; P "start"; C"or"; P"startWithCustomSpinner"; C"functions:"
            funcsOutput [{ Name = "start"; Signature = "string ->(StatusContext -> Task<unit>) -> Task<unit>" }; { Name = "startCustom"; Signature = "CustomSpinner -> (StatusContext -> Task<unit>) -> Task<unit>" }]

            C "The"; P "CustomSpinner"; C "type can be used to further customize the status widget:"
            propsOutput [
                { Name = "Message"; Type = (nameof string); Explanation = "the message displayed by the status" }
                { Name = "Spinner"; Type = (nameof Spinner); Explanation = "the Spectre spinner that should be used" }
                { Name = "Look"; Type = (nameof Look); Explanation = "the look that should be applied to the widget" }
            ]

            C "Inside the passed operation, the spinner can be updated using the"; P "update"; C"or"; P"updateWithCustomSpinner"; C"functions:"
            funcsOutput [{ Name = "update"; Signature = "string -> StatusContext -> StatusContext" }; { Name = "updateCustom"; Signature = "CustomSpinner -> StatusContext -> StatusContext" }]
        ] |> toConsole
        0