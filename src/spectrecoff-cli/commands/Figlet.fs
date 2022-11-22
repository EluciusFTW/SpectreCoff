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
        pumped "Figlet module"
        |> SpectreCoff.Rule.alignedRule Left 
        |> SpectreCoff.Rule.toConsole
        
        Many [
            CO [
                C "This module provides functionality from the figlet widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/figlet"
                C ")"
            ]
            NewLine
            C "The figlet can be used by the figlet function:"
            BI [ 
                P "figlet: string -> FigletText"
            ]
            NL
            C "This figlet will use the"
            BI [
                CO [P "Figlet.defaultAlignment,"; C "initialized to"; P "Center,"; C "and"]
                CO [P "Figlet.defaultColor,"; C "initialized to the pumped color"; P "Output.pumpedColor"; C ","]
            ]
            C "which both can be modified."
            NL
            C "Other rules can be used without changing the default by passing in the alignment as an argument to: "
            BI [ 
                P "customFiglet: Alignment -> Color -> string -> FigletText"
            ]
            NL
            CO [C "The figlet can be printed to the console with the"; P "toConsole"; C "function."]
            NL
        ] |> toConsole
        0