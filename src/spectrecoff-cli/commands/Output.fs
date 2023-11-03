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

        pumpedLook <- { pumpedLook with Color = Some Color.Fuchsia }
        edgyLook <- { edgyLook with Color = Some Color.BlueViolet }
        calmLook <- { calmLook with Color = Some Color.Green }

        NL |> toConsole

        // There are several ways to print a single line.
        // The generic way
        MCD (Color.Red, [ Decoration.Underline ], "This is underline red") |> toConsole
        MD ([ Decoration.Underline; Decoration.Dim ], "This is underline.") |> toConsole
        MC (Color.Red, "This is red") |> toConsole
        MC (Color(100uy, 200uy, 233uy), "This is RGB color") |> toConsole
        NL |> toConsole

        // The convenience way
        Calm "This utilizes the calm style. " |> toConsole
        C "C is a short for Calm." |> toConsole
        NL |> toConsole

        Pumped "This let's you use an alternate style for the line." |> toConsole
        P "P is a short for Pumped." |> toConsole
        NL |> toConsole

        Edgy "This let's you use yet another alternate style for the line." |> toConsole
        E "E is a short for Edgy." |> toConsole
        NL |> toConsole

        // Using vanilla and composite styles
        Vanilla "This let's you pass in a style as defined by Spectre." |> toConsole
        V "V is a short for Vanilla." |> toConsole
        V "In order to be able to write the styles easily, there are some functions:" |> toConsole
        V $"""You can use {markupString (Some Color.Purple) [ Decoration.Bold ] "the markup"} function,""" |> toConsole
        V $"""or {calm "the calm"}, {pumped "the pumped"} {edgy "or the edgy"} functions""" |> toConsole
        V "to utilize the same styles as defined in the current theme." |> toConsole
        V $"""As you can see, {pumped "Vanilla"} is especially useful for styles in {edgy "one line!"} (more on that below).""" |> toConsole
        NL |> toConsole

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
            NL
            C "If you want to list a few items you can use BulletItems: "
            BulletItems [
                C "listing"
                P "several"
                E "items"
            ]
            rule "Links and Emojis"
            Many [
                C "You can easily render clickable links:"
                Link "https://www.spectreconsole.net/markup"
            ]
            Many [
                C "Even with a dedicated display test:"
                LinkWithLabel ("See documentation!", "https://www.spectreconsole.net/markup")
            ]
            NL
            Many [
                C "You can use emojis by their string literals"
                Emoji "alien_monster"
            ]
            C $"""or use the constants provided by Spectre {Emoji.Known.Ghost} inline."""
            NL
        ] |> toConsole

        // Use extensions on the payload
        let payload = Many [ NL; P "Printed"; E "using"; NL; C "... the Extension!"]
        payload.toConsole

        // or, if you need to map the payload to a marked up string or renderable
        let asMarkedUpString = payload.toMarkedUpString

        // Let's test the round-trip!
        V asMarkedUpString |> toConsole
        0

open SpectreCoff.Cli.Documentation

type OutputDocumentation() =
    inherit Command<OutputSettings>()
    interface ICommandLimiter<OutputSettings>

    override _.Execute(_context, _) =
        setDocumentationStyle
        Many [
            docSynopsis "General Output" "This module contains all functions needed for output, including marking up and structuring text." "markup"
            docMissing
        ] |> toConsole
        0