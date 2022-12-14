namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type OutputSettings() =
    inherit CommandSettings()

type OutputExample() =
    inherit Command<OutputSettings>()
    interface ICommandLimiter<OutputSettings>

    override _.Execute(_context, _) =

        pumpedColor <- Color.Fuchsia
        edgyColor <- Color.BlueViolet
        calmColor <- Color.Green

        NewLine |> toConsole

        // There are several ways to print a single line.
        // The generic way
        MCS (Color.Red, Underline, "This is underline red") |> toConsole
        MS (Underline, "This is underline.") |> toConsole
        MC (Color.Red, "This is red") |> toConsole
        NewLine |> toConsole

        // The convenience way
        Calm "This utilizes the calm style. " |> toConsole
        C "C is a short for Calm." |> toConsole
        NewLine |> toConsole

        Pumped "This let's you use an alternate style for the line." |> toConsole
        P "P is a short for Pumped." |> toConsole
        NewLine |> toConsole

        Edgy "This let's you use yet another alternate style for the line." |> toConsole
        E "E is a short for Edgy." |> toConsole
        NewLine |> toConsole

        // Using vanilla and composite styles
        Vanilla "This let's you pass in a style as defined by Spectre." |> toConsole
        V "V is a short for Vanilla." |> toConsole
        V "In order to be able to write the styles easily, there are some functions:" |> toConsole
        V $"""You can use {markupString (Some Color.Purple) (Some Bold) "the markup"} function,""" |> toConsole
        V $"""or {calm "the calm"}, {pumped "the pumped"} {edgy "or the edgy"} functions""" |> toConsole
        V "to utilize the same styles as defined in the current theme." |> toConsole
        V $"""As you can see, {pumped "Vanilla"} is especially useful for styles in {edgy "one line!"} (more on that below).""" |> toConsole
        NewLine |> toConsole

        // Multiple lines at once
        [
            "Sometimes you"
            "Just want to"
            "Write a decent text"
            "Without being bothered by"
            "Symbols, types and other things ... "
        ] 
        |> List.map Calm
        |> Many
        |> toConsole

        // Compose different kinds of payloads
        Many [
            NewLine
            C "You can print many marked up lines easily as well."
            P "That is the motivation for the short alias."
            E "It preserves indentation and let's you focus on the content."
            NewLine
            CO [
                C "The CO type can be used to print"; 
                P "multiple marked up pieces"; 
                E "in one line, too." 
            ]
            NewLine
            C "Or, if you want to list a few items you can use BulletItems: "
            BulletItems [
                C "listing"
                P "several"
                E "items"
            ]
            rule "Links and Emojis"
            CO [
                C "You can easily render clickable links:"
                Link "https://www.spectreconsole.net/markup"
            ]
            CO [
                C "Even with a dedicated display test:"
                LinkWithLabel ("See documentation!", "https://www.spectreconsole.net/markup")
            ]
            NewLine
            CO [
                C "You can use emojis by their string literals"
                Emoji "alien_monster"
            ]
            C $"""or use the constants provided by Spectre {Emoji.Known.Ghost} inline."""
            NewLine
        ] |> toConsole
    
        // Use extensions on the payload
        let payload = Many [ NL; P "Printed"; E "using"; NL; C "... the Extension!"]
        payload.toConsole
    
        // or, if you need to map the payload to a marked up string or renderable
        let asRenderable = payload.toRenderable
        let asMarkedUpString = payload.toMarkedUpString

        // Let's test the round-trip!
        V asMarkedUpString |> toConsole
        0


type OutputDocumentation() =
    inherit Command<OutputSettings>()
    interface ICommandLimiter<OutputSettings>

    override _.Execute(_context, _) =
        Cli.Theme.setDocumentationStyle
        Edgy "Sorry, this documentation is not available yet." |> toConsole
        0