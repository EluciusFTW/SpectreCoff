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

open SpectreCoff.Cli.Documentation

type TextpathDocumentation() =
    inherit Command<TextpathSettings>()
    interface ICommandLimiter<TextpathSettings>

    override _.Execute(_context, _) =
        setDocumentationStyle
        Many [
            docSynopsis "Textpath module" "This module provides functionality from the text path widget of Spectre.Console" "widgets/text-path"
            BL
            C "The text path can be used by the path function:"
            BI [P "path: (value: string) -> OutputPayload"]
            BL
            C "This path will use the"; P "Textpath.defaultAlignment,"; C "which is set to"; P "Left"; C "but can be modified."
            C "Other alignments can be used without changing the default by passing in the alignment as an argument to: "
            BI [
                P "alignedPath: Alignment -> string -> OutputPayload"
            ]
            BL
            C "The path is rendered using four styles, which are mutable. The defaults are linked to the convenience style colors:"
            BI [
                define "rootColor" "(default: calmColor)"
                define "stemColor" "(default: calmColor)"
                define "separatorColor" "(default: pumperColor)"
                define "leafColor" "(default: edgyColor)"
            ]
        ] |> toConsole
        0