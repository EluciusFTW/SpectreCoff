namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type PadderSettings()  =
    inherit CommandSettings()

type PadderExample() =
    inherit Command<PadderSettings>()
    interface ICommandLimiter<PadderSettings>

    override _.Execute(_context, _settings) =

        // Let's build some boxes first
        let alienInaAbox =
            (Emoji "alien_monster")
            |> customPanel { defaultPanelLayout with Sizing = Collapse; Padding = AllEqual 0 } (pumped "Pad me!")

        // If you want to pad any output, you can simply pipe it through,
        // even multiple times
        "Invasion!"
        |> customFiglet Left Color.Purple
        |> padLeft 2
        |> padTop 5
        |> toConsole

        // These padded elements can be composed
        Many [
            for i in 1 .. 5 -> alienInaAbox |> padLeft (5*i)
        ] |> toConsole
        0

open SpectreCoff.Cli.Documentation

type PadderDocumentation() =
    inherit Command<PadderSettings>()
    interface ICommandLimiter<PadderSettings>

    override _.Execute(_context, _settings) =
        setDocumentationStyle
        Many [
            spectreDocSynopsis "Padder module" "This module provides functionality from the padder widget of Spectre.Console" "widgets/padder"
            BL
            C "Using functions from this module, any OutputPayload can be padded. The main function works similar to the one of CSS, namely it takes four padding arguments of type int as well as the element."
            BL
            BI [P "pad: top right bottom left (element: OutputPayload) -> OutputPayload"]
            BL
            C "If one does not want to set all four values differently, there are a few helpful functions that are easier to use:"
            BL
            BI [
                P "padTop: (top: int) (element: OutputPayload) -> OutputPayload"
                P "padRight: (right: int) (element: OutputPayload) -> OutputPayload"
                P "padBottom: (bottom: int) (element: OutputPayload) -> OutputPayload"
                P "padLeft: (left: int) (element: OutputPayload) -> OutputPayload"
                P "padHorizontal: (leftRight: int) (element: OutputPayload) -> OutputPayload"
                P "padVertical: (topBottom: int) (element: OutputPayload) -> OutputPayload"
                P "padSymmetric: (leftRight: int) (topBottom: int) (element: OutputPayload) -> OutputPayload"
                P "padAll: (amount: int) (element: OutputPayload) -> OutputPayload"
            ]
            BL
            C "Note that all padding functions return the element and can hence be piped. So the following produce the same result:"
            BL
            BI [
                Many [ P "element |> padHorizontal 2"; C " ~ "; P "element |> padRight 2 |> padLeft 2"]
            ]
        ] |> toConsole
        0