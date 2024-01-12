namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff

type TextpathSettings()  =
    inherit CommandSettings()

type TextpathExample() =
    inherit Command<TextpathSettings>()
    interface ICommandLimiter<TextpathSettings>

    override _.Execute(_context, _) =

        let examples = [
            "C:\\Temp\\local\\data.json"
            "C:/temp/local"
            "C:/This/Path/Is/Too/Long/To/Fit/In/The/Area/This/Path/Is/Too/Long/To/Fit/In/The/Area/This/Path/Is/Too/Long/To/Fit/In/The/Area.txt"
        ]

        alignedRule Left "Formatting examples" |> toConsole
        examples
        |> List.map path
        |> List.iter toConsole

        alignedRule Left "Alignment examples" |> toConsole
        [Left; Center; Right]
        |> List.map (fun alignment -> alignedPath alignment examples.Head)
        |> List.iter toConsole
        0
