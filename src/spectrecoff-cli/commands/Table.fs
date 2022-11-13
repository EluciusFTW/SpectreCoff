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
            Header (V "Firstname")
            CustomHeader (P "Age", { defaultColumnLayout with Alignment = Right })
        ]

        let rows = [
            [ P "11"; E "12"]
            [ C "Frank"; V "Farmington"]
        ]

        P "This shows a table with a default and custom laid-out column." |> toConsole
        let exampleTable = table headers rows
        
        exampleTable 
        |> toPayload 
        |> toConsole
        
        NewLine |> toConsole
        P "Rows can be added to the same table later on." |> toConsole
        addRow exampleTable ([P "23";  E "45"])
        exampleTable |> toPayload |> toConsole
        
        let conversions = 
            [ [ V "1"; V "5"]; [ V "2.54"; V "12.7"] ] 
            |> table [Header (P "Inches"); Header (P "Centimetres") ] 
            |> toPayload

        let customLayout = 
            { HideHeaders = true; 
              Border = TableBorder.Minimal; 
              Sizing = Collapse;
              Alignment = Right }

        let nestedTable = 
            [ [ C "Let's nest"; P "some TABLES!" ] 
              [ conversions;  exampleTable |> toPayload ] ]
            |> customTable customLayout [ Header (V "");  Header (V "") ] 
            |> toPayload

        ManyMarkedUp [
            NewLine
            C "A few more things to mention"
            BI [
                P "Tables can be nested, or contain other markup" 
                P "The table can be customized, too!"
                P "Also, since the table is just another type of payload, you can compose your content in one go!"
            ]
            nestedTable
        ] |> toConsole
        0

open SpectreCoff.Cli

type TableDocumentation() =
    inherit Command<TableSettings>()
    interface ICommandLimiter<TableSettings>

    override _.Execute(_context, _) =
        Theme.setDocumentationStyle
        Edgy "Sorry, this documentation is not available yet." |> toConsole    
        0
