namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open Spectre.Console

open SpectreCoff.Layout
open SpectreCoff.Figlet

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

open SpectreCoff.Cli
open SpectreCoff.Output

type FigletDocumentation() =
    inherit Command<FigletSettings>()
    interface ICommandLimiter<FigletSettings>

    override _.Execute(_context, _settings) =
        Theme.setDocumentationStyle

        NewLine |> toConsole
        emphasize "Figlet module"
        |> SpectreCoff.Rule.alignedRule Left 
        |> SpectreCoff.Rule.toConsole
        
        ManyMarkedUp [
            CO [
                S "This module provides functionality from the figlet widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/figlet"
                S ")"
            ]
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