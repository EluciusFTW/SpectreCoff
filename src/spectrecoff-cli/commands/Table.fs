namespace SpectreCoff.Cli.Commands

open Spectre.Console;
open Spectre.Console.Cli
open SpectreCoff

type TableSettings()  =
    inherit CommandSettings()

type TableExample() =
    inherit Command<TableSettings>()
    interface ICommandLimiter<TableSettings>

    override _.Execute(_context, _) =

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
            BL
        ] |> toConsole

        [ for i in 6 .. 10 -> Numbers [i; pown i 2; pown i 3] ] |> List.iter exampleTable.addRow
        Many [
            P "Rows can be added to the same table later on."
            exampleTable.toOutputPayload
            BL
        ] |> toConsole

        // Now for a more complete example
        let columns = [
            column (Calm "Results")
            column (Pumped "Interpretations")
            |> withFooter (Pumped "Footer works, too!")
            |> withLayout { defaultColumnLayout with Alignment = Right }
        ]

        [ Payloads [ exampleTable.toOutputPayload;  MCD (Color.Red, [ Decoration.Bold ], "The bigger the exponent, the faster the sequence grows.") ]
          Payloads [ P "Under the table"; panel "Wow" (E "ye-haw") ]
          Strings [ "Sum"; "Last" ]
          Numbers [ 55; 10 ] ]
        |> customTable { defaultTableLayout with Sizing = Collapse; Border = TableBorder.DoubleEdge; HideHeaders = true } columns
        |> withTitle "Tables can be nested, contain other Payloads, have titles ..."
        |> withCaption "..., have footers, captions, be customized, ..."
        |> toOutputPayload
        |> toConsole
        0
