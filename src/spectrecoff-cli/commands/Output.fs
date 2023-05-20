namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type OutputSettings() =
    inherit CommandSettings()

type OutputExample() =
    inherit Command<OutputSettings>()
    interface ICommandLimiter<OutputSettings>

    override _.Execute(_context, _) =

        pumpedColor <- Color.Fuchsia
        edgyColor <- Color.BlueViolet
        calmColor <- Color.Green

        let columns = [
            column (Calm "Number")
            column (Calm "Square")
            column (Pumped "Cube") |> withLayout { defaultColumnLayout with Alignment = Right }
        ]
        let rows = [ for i in 1 .. 5 -> Numbers [i; pown i 2; pown i 3] ]

        let exampleTable = table columns rows
        Many [
            P "This shows a table with a default and custom laid-out column."
            exampleTable.toOutputPayload
            NewLine
        ] |> toConsole

        Many [C "This should"; C "Render in";P "One Line"; exampleTable.toOutputPayload] |> toConsole
        0


type OutputDocumentation() =
    inherit Command<OutputSettings>()
    interface ICommandLimiter<OutputSettings>

    override _.Execute(_context, _) =
        Cli.Theme.setDocumentationStyle
        Edgy "Sorry, this documentation is not available yet." |> toConsole
        0