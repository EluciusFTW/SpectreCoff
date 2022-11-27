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
            BI [
                C "Composability over inheritance" 
                P "Make illegal states irrepresentable"
                P "Types are your friend"
                C "Immutability over, well, mutability"
                C "Declarative over imperative"
            ]
            
        let header = 
            P "Guiding principles "
            |> toMarkedUpString

        principles
        |> toMarkedUpString 
        |> panel header 
        |> toConsole    

        pumped "That surrounding border can be customized easily, e.g., to take up as much horizontal space as needed."
        |> customPanel { defaultPanelLayout with Sizing = Expand } " Customization " 
        |> toConsole
        0

type PanelDocumentation() =
    inherit Command<PanelSettings>()
    interface ICommandLimiter<PanelSettings>
    
    override _.Execute(_context, _) = 
        Cli.Theme.setDocumentationStyle
        0
