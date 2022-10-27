namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff.Layout
open SpectreCoff.Figlet
open SpectreCoff.Output
open SpectreCoff.Cli
open Spectre.Console

type FigletSettings()  =
    inherit CommandSettings()

type FigletExample() =
    inherit Command<FigletSettings>()
    interface ICommandLimiter<FigletSettings>

    override _.Execute(_context, _settings) =
        "Star ..." 
        |> customFiglet Left Color.SeaGreen1 
        |> SpectreCoff.Figlet.toConsole
        
        "Wars!" 
        |> figlet
        |> SpectreCoff.Figlet.toConsole
        0

type FigletDocumentation() =
    inherit Command<FigletSettings>()
    interface ICommandLimiter<FigletSettings>

    override _.Execute(_context, _settings) =
        
        Theme.setDocumentationStyle
        NewLine |> toConsole
        SpectreCoff.Rule.alignedRule Left (emphasize "Figlet module") |> SpectreCoff.Rule.toConsole
        
        ManyMarkedUp [
            CO [S "This module provides functionality from the "; E "figlet widget"; S " of Spectre.Console"]
            NewLine
            S "The figlet can be used by the figlet function:"
            BI [ 
                E "figlet: string -> FigletText"
            ]
            NL
            S "This figlet will use the"
            BI [
                CO [E "Figlet.defaultAlignment"; S ", initialized to "; E "Center"; S ", and"]
                CO [E "Figlet.defaultColor"; S ", initialized to the emphasize color "; E "Output.emphasizeColor"; S ","]
            ]
            S "which both can be modified."
            NL
            S "Other rules can be used without changing the default by passing in the alignment as an argument to: "
            BI [ 
                E "customFiglet: Alignment -> Color -> string -> FigletText"
            ]
            NL
            CO [S "The figlet can be printed to the console with the "; E "toConsole"; S " function."]
            NL
        ] |> toConsole
        0