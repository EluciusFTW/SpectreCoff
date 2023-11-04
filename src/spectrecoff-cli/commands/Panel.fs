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

open SpectreCoff.Cli.Documentation

type PanelDocumentation() =
    inherit Command<PanelSettings>()
    interface ICommandLimiter<PanelSettings>

    override _.Execute(_context, _) =
        setDocumentationStyle
        Many [
            docSynopsis "Panel module" "This module provides functionality from the panel widget of Spectre.Console" "widgets/panel"
            BL
            C "The panel can be used by the panel function:"
            BI [P "panel: (header: string) -> (content: OutputPayload) -> OutputPayload"]
            BL
            C "While the content can be an arbitrary payload"
            C "the header needs to be a string."
            C "Spectre still will render markup here, though, so all"
            P "stringifyable payloads"
            C "can be used here when mapped to string using the extension"
            P "payload.toMarkedUpString."
            BL
            C "This panel will use the"; P "Panel.defaultPanelLayout,"; C "which has the values:"
            BI [
                P "Border: BorderBox.Rounded"
                P "BorderColor: edgyColor"
                P "Sizing: Collapse"
                P "Padding: AllEqual 2"
            ]
            BL
            C "In order to produce a panel with other layout, the default values can be modified. This changes the layout for all subsequent panels"
            C "If the alternative layout shall only be applied to an individual panel, it can be passed to this function:"
            BI [P "customPanel: PanelLayout -> string -> OutputPayload -> OutputPayload"]
        ] |> toConsole
        0
