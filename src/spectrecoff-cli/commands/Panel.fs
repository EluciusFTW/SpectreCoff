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
        
        let content = "Important text can be highlighted by surrounding it with a border and title." 
        let title = "Note !"

        // content
        // |> vanillaPanel title
        // |> SpectreCoff.Panel.toConsole    

        // "That surrounding border can be customized easily, e.g., to take up as much horizontal space as needed."
        // |> customPanel { defaultPanelLayout with Sizing = Expand } "Customized !"  
        // |> toConsole

        // SpectreCoff.Output.Link "www.wikipedia.com"
        // |> cP defaultPanelLayout (SpectreCoff.Output.Emoji "ghost")
        // |> toConsole

        let panel = 
            P "a lot of stuuf"
            |> vP (V "content")
        
        ManyMarkedUp 
            [ P "Demo new panel"
              panel ] 
            |> toConsole
        0

open SpectreCoff.Cli
open SpectreCoff.Rule
open SpectreCoff.Output

type PanelDocumentation() =
    inherit Command<PanelSettings>()
    interface ICommandLimiter<PanelSettings>
    
    override _.Execute(_context, _) = 
        Theme.setDocumentationStyle

        NewLine |> toConsole
        pumped "Panel module"
        |> alignedRule Left 
        |> SpectreCoff.Rule.toConsole
        
        ManyMarkedUp [
            CO [
                C "This module provides functionality from the panel widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/panel"
                C ")"
            ]
            NewLine
            C "The panel can be used by the panel function:"
            BI [ P "panel: (title: string) -> (content: string) -> Panel" ]
            NL
            CO [C "This panel will use the "; P "Panel.defaultPanelLayout: PanelLayout"; C " which is a record with"]
            BI [
                P "Border: Spectre.Console.BoxBorder"
                P "Sizing: Layout.SizingBehaviour"
                P "Padding: PanelLayout.Padding"
            ]
            NL
            C "A custom layout instance can be passed without changing the default by passing it as an argument to: "
            BI [ 
                P "customPanel: PanelLayout -> string -> string -> Panel"
            ]
            NL
            CO [C "The panel can be printed to the console with the "; P "toConsole"; C " function."]
            NL
        ] |> toConsole
        0
