namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff.Table
open SpectreCoff.Output
open SpectreCoff.Layout

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
            Numbers [1; 2]
            Strings ["Alfons"; "Schubeck"]
        ]

        let table = table headers rows
        table |> SpectreCoff.Table.toConsole

        E "Nested tables" |> toConsole
        
        let some = markupString "red" "bold" "some text" |> markup

        let nextRows = [
            Row.Renderables [ table;  some ]
            Payloads [ E "Let's"; W " Go!" ] 
        ]

        (table headers nextRows) |> SpectreCoff.Table.toConsole
        0