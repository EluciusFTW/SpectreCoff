namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type FigletSettings()  =
    inherit CommandSettings()

type FigletExample() =
    inherit Command<FigletSettings>()
    interface ICommandLimiter<FigletSettings>

    override _.Execute(_context, _settings) =
        "Star ..."
        |> customFiglet Left Color.SeaGreen1
        |> toConsole

        "Wars!"
        |> figlet
        |> toConsole
        0

open SpectreCoff.Cli.Documentation

type FigletDocumentation() =
    inherit Command<FigletSettings>()
    interface ICommandLimiter<FigletSettings>

    override _.Execute(_context, _settings) =
        setDocumentationStyle
        Many [
            spectreDocSynopsis "Figlet module" "This module provides functionality from the figlet widget of Spectre.Console" "widgets/figlet"
            C "The figlet can be created with one of the following functions:"
            funcsOutput [
                { Name = "figlet"; Signature = "string -> OutputPayload" }
                { Name = "customFiglet"; Signature = "Alignment -> Color -> string -> OutputPayload" }
            ]
            BL
            C "While the custom figlet accepts color and alignement as inputs, the default figlet will use these modifiable variables instead:"
            valuesOutput [
                { Name = "defaultAlignment"; Type ="Alignment"; DefaultValue = "Center"; Explanation = "The alignment of the figlet text" }
                { Name = "defaultColor"; Type ="Color Option"; DefaultValue = "pumpedColor"; Explanation = "The color of the figlet" }
            ]
            BL
            C "The figlet can be printed to the console with the"; P "toConsole"; C "function."
        ] |> toConsole
        0