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
