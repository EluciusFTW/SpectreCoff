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
            spectreDocSynopsis "Panel module" "This module provides functionality from the panel widget of Spectre.Console" "widgets/panel"
            C "Panels can be created using one of these functions:"
            funcsOutput [
                { Name = "panel"; Signature = "(header: string) -> (content: OutputPayload) -> OutputPayload" }
                { Name = "customPanel"; Signature = "PanelLayout -> string -> OutputPayload -> OutputPayload" }
            ]
            BL
            C "While the content can be an arbitrary payload the header needs to be a string."
            C "Spectre still will render markup here, though, so all"
            P "stringifyable payloads"
            C "can be used here when mapped to string using the extension"
            P "payload.toMarkedUpString."
            BL
            C "This panel will use the"; P "Panel.defaultPanelLayout,"; C "which has the properties:"
            valuesOutput [
                { Name = "Border"; Type = "BoxBorder"; DefaultValue = "BorderBox.Rounded"; Explanation = "The style of the border" }
                { Name = "BorderColor"; Type = "Color Option"; DefaultValue = "edgyColor"; Explanation = "The (optional) color of the border" }
                { Name = "Sizing"; Type = "SizingBehaviour"; DefaultValue = "Collapse"; Explanation = "Determines the space the panel takes up" }
                { Name = "Padding"; Type = "Padding"; DefaultValue = "AllEqual 2"; Explanation = "The padding around the content of the panel" }
            ]
            BL
            C "As always, the defaults can be modified, and this changes the layout for all subsequent panels."
            C "For a one-off change, an instance of PanelLayout can be passed to the customPanle function as well."
        ] |> toConsole
        0
