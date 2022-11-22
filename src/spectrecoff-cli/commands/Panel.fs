namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff.Panel
open SpectreCoff.Layout
open SpectreCoff.Output

type PanelSettings()  =
    inherit CommandSettings()

type PanelExample() =
    inherit Command<PanelSettings>()
    interface ICommandLimiter<PanelSettings>

    override _.Execute(_context, _) = 
        let principles =  BI [
            C "Composability over Inheritance" 
            P "Make illegal states irrepresentable"
            P "Types are your friend"
            C "Immutability over, well, mutability"
            C "Declarative over Imperative"
        ]
        panel (P " Guiding principles ") (principles) 
        |> toConsole    

        customPanel { defaultPanelLayout with Sizing = Expand } (E "Custom !!") (P "That surrounding border can be customized easily, e.g., to take up as much horizontal space as needed.") 
        |> toConsole
        0

open SpectreCoff.Cli

type PanelDocumentation() =
    inherit Command<PanelSettings>()
    interface ICommandLimiter<PanelSettings>
    
    override _.Execute(_context, _) = 
        Theme.setDocumentationStyle
        0
