namespace SpectreCoff.Cli.Commands

open Dumpify
open Spectre.Console.Cli
open SpectreCoff.Dumpify

type DumpifySettings() =
    inherit CommandSettings()

type DumpifyExample() =
    inherit Command<DumpifySettings>()
    interface ICommandLimiter<DumpifySettings>

    override _.Execute(_context, _settings) =
        // use the dump function to print all kinds of objects
        [|1;2;3|] |> dump |> ignore
        {| Diameter = 5.5; Area = 3.3; Circumference = 2.2 |} |> dump |> ignore

        // use the customDump function to fine-tune the parameters for your dumping
        let colorConfig = ColorConfig()
        colorConfig.PropertyValueColor <- DumpColor("#FF0000")
        let options = { defaultOptions with Color =  Some colorConfig }
        customDump options {| Diameter = 5.5; Area = 3.3; Circumference = 2.2 |} |> ignore
        0
