namespace SpectreCoff.Cli.Commands

open Spectre.Console
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

type TextpathDocumentation() =
    inherit Command<TextpathSettings>()
    interface ICommandLimiter<TextpathSettings>

    override _.Execute(_context, _) =

        Cli.Theme.setDocumentationStyle

        EmptyLine |> toConsole
        pumped "Textpath module"
        |> alignedRule Left
        |> toConsole

        Many [
            Many [
                C "This module provides functionality from the text path widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/text-path"
                C ")"
            ]
            EL
            C "The text path can be used by the path function:"
            BI [P "path: (value: string) -> OutputPayload"]
            EL
            Many [C "This path will use the"; P "Textpath.defaultAlignment,"; C "which is set to"; P "Left"; C "but can be modified."]
            C "Other alignments can be used without changing the default by passing in the alignment as an argument to: "
            BI [
                P "alignedPath: Alignment -> string -> OutputPayload"
            ]
            EL
            C "The path is rendered using four styles, which are mutable. The defaults are linked to the convenience style colors:"
            BI [
                Many [P "rootColor"; C  "(default: calmColor)"]
                Many [P "stemColor"; C  "(default: calmColor)"]
                Many [P "separatorColor"; C  "(default: pumperColor)"]
                Many [P "leafColor"; C  "(default: edgyColor)"]
            ]
        ] |> toConsole
        0