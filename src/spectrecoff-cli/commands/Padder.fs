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
            C "Using functions from this module, any OutputPayload can be padded"
            BL
            funcsOutput [
                { Name = "pad"; Signature = "(top: int) -> (right: int) -> (bottom: int) -> (left: int) -> OutputPayload -> OutputPayload" }
                { Name = "padTop"; Signature = "int -> OutputPayload -> OutputPayload" }
                { Name = "padRight"; Signature = "int -> OutputPayload -> OutputPayload" }
                { Name = "padBottom"; Signature = "int -> OutputPayload -> OutputPayload" }
                { Name = "padLeft"; Signature = "int -> OutputPayload -> OutputPayload" }
                { Name = "padHorizontal"; Signature = "int -> OutputPayload -> OutputPayload" }
                { Name = "padVertical"; Signature = "int -> OutputPayload -> OutputPayload" }
                { Name = "padSymmetric"; Signature = "(leftRight: int) -> (topBottom: int) -> OutputPayload -> OutputPayload" }
                { Name = "padAll"; Signature = "int -> OutputPayload -> OutputPayload" }
            ]
            BL
            C "Note that all padding functions return the element and can hence be piped. So the following produce the same result:"
            BL
            BI [
                Many [ P "element |> padHorizontal 2"; C " ~ "; P "element |> padRight 2 |> padLeft 2"]
            ]
        ] |> toConsole
        0