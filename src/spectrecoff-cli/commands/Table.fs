namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff.Table
open SpectreCoff.Output
open SpectreCoff.Layout
open Spectre.Console

type TableExampleSettings()  =
    inherit CommandSettings()

type TableExample() =
    inherit Command<TableExampleSettings>()
    interface ICommandLimiter<TableExampleSettings>

    override _.Execute(_context, _) =

        let headers = [
            DefaultHeader (Simple "Firstname")
            CustomHeader ((Simple "Age"), { defaultColumnLayout with Alignment = Right })
        ]

        let rows = [
            Numbers [11; 37]
            Strings ["Frank"; "Farmington"]
        ]

        E "This shows a table with a default and custom laid-out column." |> toConsole
        let exampleTable = table headers rows
        exampleTable |> SpectreCoff.Table.toConsole
        
        NewLine |> toConsole
        E "Rows can be added to the same table later on." |> toConsole
        addRow exampleTable (Numbers [23; 45])
        exampleTable |> SpectreCoff.Table.toConsole

        NewLine |> toConsole
        E "Tables can be nested, or contain other markup" |> toConsole
        
        let exampleMarkup = 
            "some text" 
            |> markupString (Some Color.Red) (Some Bold)
            |> toMarkup

        [ Renderables [ exampleTable;  exampleMarkup ]
          Payloads [ E "Let's"; W " Go!" ] ] 
            |> table headers 
            |> SpectreCoff.Table.toConsole

        NewLine |> toConsole
        E "The table can be customized, too." |> toConsole
        customTable  
            { defaultTableLayout with Sizing = Collapse; Border = TableBorder.Minimal } headers rows 
            |> SpectreCoff.Table.toConsole
        
        0