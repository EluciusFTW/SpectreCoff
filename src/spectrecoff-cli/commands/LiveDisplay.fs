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
            spectreDocSynopsis "Live Display module" "This module provides functionality from the live display widget of Spectre.Console" "widgets/live-display"
            docMissing
        ] |> toConsole
        0