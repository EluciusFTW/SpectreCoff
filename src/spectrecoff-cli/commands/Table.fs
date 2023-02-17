namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type TableSettings()  =
    inherit CommandSettings()

type TableExample() =
    inherit Command<TableSettings>()
    interface ICommandLimiter<TableSettings>

    override _.Execute(_context, _) =

        let columns = [
            Calm "Number"
            Calm "Square"
            Calm "Cube"
        ]         
        let definitions = toColumnDefinitions columns
        let rows = [ for i in 1 .. 5 -> Numbers [i; pown i 2; pown i 3] ]
        
        let exampleTable = table definitions rows
        Many [
            P "This shows a table with a default and custom laid-out column."
            exampleTable.toOutputPayload
            NewLine
        ] |> toConsole

        [ for i in 6 .. 10 -> Numbers [i; pown i 2; pown i 3] ] |> List.iter exampleTable.addRow
        Many[
            P "Rows can be added to the same table later on."
            exampleTable.toOutputPayload
            NewLine
        ] |> toConsole

        P "Tables can be nested, contain other Payloads, and be customized" |> toConsole
        let columns = [
            toLaidOutColumn defaultColumnLayout (Calm "Results")
            toLaidOutColumn { defaultColumnLayout with Alignment = Right } (Pumped "Interpretations")
        ]
        let rows = [ 
            Payloads [ exampleTable.toOutputPayload;  MCS (Color.Red, Bold, "The bigger the exponent, the faster the sequence grows.") ]
            Payloads [ P "Under the table"; panel "Wow" (E "ye-haw")] 
            Strings ["Sum"; "Last"]
            Numbers [ 55; 10]
        ]

        rows
            |> customTable { defaultTableLayout with Sizing = Collapse; Border = TableBorder.DoubleEdge } columns
            |> toOutputPayload
            |> toConsole
        0

type TableDocumentation() =
    inherit Command<TableSettings>()
    interface ICommandLimiter<TableSettings>

    override _.Execute(_context, _) =
        Cli.Theme.setDocumentationStyle
        Edgy "Sorry, this documentation is not available yet." |> toConsole
        0
