namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff.Table
open SpectreCoff.Output
open SpectreCoff.Layout
open Spectre.Console

type TableSettings()  =
    inherit CommandSettings()

type TableExample() =
    inherit Command<TableSettings>()
    interface ICommandLimiter<TableSettings>

    override _.Execute(_context, _) =

        let headers = [
            DefaultHeader (Simple "Firstname")
            CustomHeader ((Simple "Age"), { defaultColumnLayout with Alignment = Right })
        ]

        let rows = [
            Numbers [11; 37]
            Strings ["Frank"; "Farmington"]
        ]

        P "This shows a table with a default and custom laid-out column." |> toConsole
        let exampleTable = table headers rows
        exampleTable |> SpectreCoff.Table.toConsole
        
        NewLine |> toConsole
        P "Rows can be added to the same table later on." |> toConsole
        addRow exampleTable (Numbers [23; 45])
        exampleTable |> SpectreCoff.Table.toConsole

        NewLine |> toConsole
        P "Tables can be nested, or contain other markup" |> toConsole
        
        let exampleMarkup = 
            "some text" 
            |> markupString (Some Color.Red) (Some Bold)
            |> toMarkup

        [ Renderables [ exampleTable;  exampleMarkup ]
          Payloads [ P "Let's"; E " Go!" ] ] 
            |> table headers 
            |> SpectreCoff.Table.toConsole

        NewLine |> toConsole
        P "The table can be customized, too." |> toConsole
        customTable  
            { defaultTableLayout with Sizing = Collapse; Border = TableBorder.Minimal } headers rows 
            |> SpectreCoff.Table.toConsole
        
        0

open SpectreCoff.Cli

type TableDocumentation() =
    inherit Command<TableSettings>()
    interface ICommandLimiter<TableSettings>

    override _.Execute(_context, _) =
        Theme.setDocumentationStyle
        Edgy "Sorry, this documentation is not available yet." |> toConsole    
        0
