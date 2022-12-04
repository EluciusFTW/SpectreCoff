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
                rule " and somctimes "
                figlet "Readability over performance"
                P "... but not always, duh."
            ]
            
        let header = 
            P " Guiding principles "
            |> toMarkedUpString

        principles
        |> panel header 
        |> toConsole    

        P "That surrounding border can be customized easily, e.g., to take up as much horizontal space as needed."
        |> customPanel { defaultPanelLayout with Sizing = Expand } " Customization " 
        |> toConsole
        0

type PanelDocumentation() =
    inherit Command<PanelSettings>()
    interface ICommandLimiter<PanelSettings>
    
    override _.Execute(_context, _) = 
        Cli.Theme.setDocumentationStyle

        NewLine |> toConsole
        pumped "Panel module"
        |> alignedRule Left
        |> toConsole

        Many [
            CO [
                C "This module provides functionality from the panel widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/panel"
                C ")"
            ]
            NL
            C "The panel can be used by the panel function:"
            BI [P "panel: (header: string) -> (content: OutputPayload) -> OutputPayload"]
            NL
            CO [
                C "While the content can be an arbitrary payload" 
                C "the header needs to be a string."
                C "Spectre still will render markup here, though, so all"; 
                P "stringifyable payloads"; 
                C "can be used here when mapped to string using the extension"; 
                P "payload.toMarkedUpString."
            ]
            NL
            CO [
                C "This panel will use the"; 
                P "Panel.defaultPanelLayout,"; 
                C "initialized to heavy border, collapsed expansion behaviour and a padding of 2 units on all sides."
            ]
            NL
            C "In order to produce a panel with other layout, the function"
            BI [P "customPanel: PanelLayout -> string -> OutputPayload -> OutputPayload"]
            NL
            C "can be used. Alternatively, the default can also be modified."
            NL
        ] |> toConsole
        0
