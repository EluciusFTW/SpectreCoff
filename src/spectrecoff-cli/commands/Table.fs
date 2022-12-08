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

        let headers = [
            DefaultHeader (Calm "Number")
            DefaultHeader (Calm "Square")
            CustomHeader ((Calm "Cube"), { defaultColumnLayout with Alignment = Right })
        ]

        let rows = [
            for i in 1 .. 10 -> Numbers [i; pown i 2; pown i 3]
        ]

        P "This shows a table with a default and custom laid-out column." |> toConsole
        let exampleTable = table headers rows
        exampleTable.toOutputPayload |> toConsole
        
        NewLine |> toConsole
        P "Rows can be added to the same table later on." |> toConsole
        Numbers [20; pown 20 2; pown 20 3] |> addRow exampleTable
        exampleTable
        |> toOutputPayload
        |> toConsole

        NewLine |> toConsole
        P "Tables can be nested, contain other Payloads, and be customized" |> toConsole
        
        let exampleMarkup = 
            "The bigger the exponent, the faster the sequence grows." 
            |> markupString (Some Color.Red) (Some Bold)
            |> Markup

        [ Renderables [ exampleTable;  exampleMarkup ]
          Payloads [ P ""; figlet "Wow"] ] 
            |> customTable 
                { defaultTableLayout with Sizing = Collapse; Border = TableBorder.DoubleEdge } 
                [DefaultHeader (Calm "Results"); DefaultHeader (Pumped "Interpretations")] 
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
