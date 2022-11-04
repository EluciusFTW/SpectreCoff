namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff.Layout
open SpectreCoff.Output

type OutputSettings() =
    inherit CommandSettings()
   
type OutputExample() =
    inherit Command<OutputSettings>()
    interface ICommandLimiter<OutputSettings>

    override _.Execute(_context, _) = 

        emphasizeColor <- Color.Fuchsia
        warningColor <- Color.BlueViolet
        standardColor <- Color.Green
        
        NewLine |> toConsole
        
        // There are several ways to print a single line.
        // Generic way
        MCS (Color.Red, Underline, "This is underline red") |> toConsole
        MS (Underline, "This is underline.") |> toConsole
        MC (Color.Red, "This is red") |> toConsole
        NewLine |> toConsole

        // Convenience way
        Standard "This utilizes the standard style. " |> toConsole
        S "S is a short for Standard." |> toConsole
        NewLine |> toConsole
        
        Emphasize "This let's you use an alternate style for the line." |> toConsole
        E "E is a short for Emphasize." |> toConsole
        NewLine |> toConsole
        
        Warn "This let's you use an alternate style for the line." |> toConsole
        W "W is a short for Warn." |> toConsole
        NewLine |> toConsole

        // Custom and composite styles
        Custom "This let's you pass in a complete custom style as defined by Spectre." |> toConsole
        C "C is a short for Custom." |> toConsole
        C "In order to be able to write the styles easily, there are some functions:" |> toConsole
        C $"""You cau use {markupString (Some Color.Purple) (Some Bold) "the markup"} function,""" |> toConsole
        C $"""or {standard "the emphasize"}, {emphasize "the emphasize"} {warn "warn"} functions""" |> toConsole
        C "to utilize the same styles as defined in the current theme." |> toConsole
        C $"""As you can see, {emphasize "Custom"} is especially useful for  styles in {warn "one line!"} (more on that below).""" |> toConsole
        NewLine |> toConsole

        // You can print multiple lines at once very easily as well:
        Many [
            "Sometimes you"
            "Just want to"
            "Write a decent text"
            "Without being bothered by"
            "Symbols, types and other things ... "
        ] |> toConsole
        NewLine |> toConsole

        // You can markup each of the lines individually:
        ManyMarkedUp [
            S "You can print many marked up lines easily as well."
            NewLine
            E "That is the motivation for the short"
            W "No need to escape markup characters from strings [ ... /] manually." 
            CO [
                S "The CO type can be used to print "; 
                E "multiple marked up pieces"; 
                W " in one line, too." 
            ]
            NewLine
        ] |> toConsole 

        // There are special payloads for links and emojis:
        ManyMarkedUp [
            CO [
                S "You can easily render clickable links: "
                Link "https://www.spectreconsole.net/markup"
            ]
            CO [
                S "You can add a label as well: "
                LinkWithLabel ("See documentation!", "https://www.spectreconsole.net/markup")
            ]
            CO [
                S "You can also use emojis by their string literals "
                Emoji "alien_monster"
            ]
            S $"""or use the constants provided by Spectre {Emoji.Known.Ghost} inline."""
            NewLine
        ] |> toConsole

        // You can also easily print bullet items:
        bulletItemPrefix <- "  ->> "
        ManyMarkedUp [
            S "Another common use-case are bullet-items: "
            BulletItems [
                S "listing"
                E "several"
                W "items"
            ]
        ] |> toConsole 
        0

open SpectreCoff.Cli

type OutputDocumentation() =
    inherit Command<OutputSettings>()
    interface ICommandLimiter<OutputSettings>

    override _.Execute(_context, _) =
        Theme.setDocumentationStyle
        Warn "Sorry, this documentation is not available yet." |> toConsole    
        0