namespace SpectreCoff.Cli.Commands

open System.Threading.Tasks
open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type LiveDisplaySettings()  =
    inherit CommandSettings()

type LiveDisplayExample() =
    inherit Command<LiveDisplaySettings>()
    interface ICommandLimiter<LiveDisplaySettings>

    override _.Execute(_context, _settings) =
        let columns = [
            column (Calm "Number")
            column (Calm "Square")
            column (Pumped "Cube") |> withLayout { defaultColumnLayout with Alignment = Right }
        ]
        let exampleTable = table columns []

        let addRow index table =
            Numbers [index; pown index 2; pown index 3] |> addRowToTable table

        let operation (context: LiveDisplayContext) =
            task {
                 for i in 1 .. 20 do
                    exampleTable |> addRow i
                    context.Refresh()
                    do! Task.Delay(200)
            }

        let config = { defaultConfiguration with Overflow = Some VerticalOverflow.Ellipsis }
        (startWithCustomConfig config exampleTable operation).Wait()
        0

open SpectreCoff.Cli.Documentation

type LiveDisplayDocumentation() =
    inherit Command<LiveDisplaySettings>()
    interface ICommandLimiter<LiveDisplaySettings>

    override _.Execute(_context, _settings) =
        setDocumentationStyle
        Many [
            spectreDocSynopsis "Live Display module" "This module provides functionality from the live display element of Spectre.Console" "live/live-display"

            C "Live display can be started by passing an"; P "IRenderable"; C "to the"; P "start";C "or the"; P "startWithCustomConfig"; C "functions:"
            funcsOutput [{ Name = "start"; Signature = "IRenderable -> (LiveDisplayContext -> Task<unit>) -> Task<unit>" }]

            C "Any changes to the IRenderable will be reflected in real time"
            BL
            C "When using the"; P "startWithCustomConfig"; C "function, the"; P "Configuration"; C "type can be used to further customize the live display:"
            propsOutput [
                { Name = "AutoClear"; Type = (nameof string); Explanation = "defines if the console should be cleared before starting" }
                { Name = "Overflow"; Type = (nameof Spinner); Explanation = "the overflow behavior" }
                { Name = "Cropping"; Type = (nameof Look); Explanation = "the cropping behavior" }
            ]
        ] |> toConsole
        0