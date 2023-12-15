namespace SpectreCoff.Cli.Commands

open Dumpify
open Spectre.Console.Cli
open SpectreCoff
open SpectreCoff.Dumpify

type DumpifySettings() =
    inherit CommandSettings()

type DumpifyExample() =
    inherit Command<DumpifySettings>()
    interface ICommandLimiter<DumpifySettings>

    override _.Execute(_context, _settings) =
        // use the dump function to print all kinds of objects
        let list = [|1;2;3|]
        dump list |> ignore
        {| Diameter = 5.5; Area = 3.3; Circumference = 2.2 |} |> dump |> ignore

        // use the customDump function to fine-tune the parameters for your dumping
        let colorConfig = ColorConfig()
        colorConfig.PropertyValueColor <- DumpColor("#FF0000")
        let options = { defaultOptions with Color =  Some colorConfig }
        customDump options {| Diameter = 5.5; Area = 3.3; Circumference = 2.2 |} |> ignore
        0

open SpectreCoff.Cli.Documentation

type DumpifyDocumentation() =
    inherit Command<DumpifySettings>()
    interface ICommandLimiter<DumpifySettings>

    override _.Execute(_context, _settings) =
        setDocumentationStyle
        Many [
            docSynopsis "Dumpify module" "This module provides functionality from the Dumpify package" "https://github.com/MoaidHathot/Dumpify"
            C "Payload can be dumped using one of these functions:"
            funcsOutput [
                { Name = "dump"; Signature = "'a -> 'a" }
                { Name = "customDump"; Signature = "DumpifyOptions -> 'a -> 'a" }
            ]
            BL
            C "Just like the wrapped package, the function will print the payload's properties to the console."
            BL
            C "dump will use the"; P "Dumpify.defaultOptions,"; C "which will set all properties to"; P "None,"; C "resulting in the same behavior as the wrapped package."
            BL
            C "As always, the defaults can be modified and passed to the"; P "customDump"; C "function."
            C "Each member of the"; P "DumpifyOptions"; C "type mirrors the respective parameter from the wrapped package, resulting in the same behavior when changed."
        ] |> toConsole
        0