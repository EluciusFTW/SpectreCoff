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
        |> figlet
        |> padleft 3
        |> padtop 2
        |> toConsole

        // These padded elements can be composed
        Many [ 
            for i in 1 .. 8 -> alienInaAbox |> padleft (24 - 3*i) 
        ] |> toConsole
        0

type PadderDocumentation() =
    inherit Command<PadderSettings>()
    interface ICommandLimiter<PadderSettings>

    override _.Execute(_context, _settings) =
        Cli.Theme.setDocumentationStyle

        NewLine |> toConsole
        pumped "Padder module"
        |> alignedRule Left
        |> toConsole

        Many [
            CO [
                C "This module provides functionality from the figlet widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/padder"
                C ")"
            ]
            NL
            Edgy "Sorry, this documentation is not available yet."
        ] |> toConsole
        0