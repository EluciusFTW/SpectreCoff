namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff

type PanelSettings()  =
    inherit CommandSettings()

type PanelExample() =
    inherit Command<PanelSettings>()
    interface ICommandLimiter<PanelSettings>

    override _.Execute(_context, _) =
        let principles =
            Many [
                BI [
                    C "Composability over inheritance"
                    P "Make illegal states irrepresentable"
                    P "Types are your friend"
                    C "Immutability over, well, mutability"
                    C "Declarative over imperative"
                ]
                rule " and sometimes "
                figlet "Readability over performance"
                P "... but not always, duh."
            ]

        let header =
            P " Guiding principles "
            |> toMarkedUpString

        principles
        |> panel header
        |> toConsole

        P "That surrounding border can be customized easily!"
        |> customPanel { defaultPanelLayout with Sizing = Expand; BorderColor = Some Spectre.Console.Color.Yellow } " Customization "
        |> toConsole
        0
