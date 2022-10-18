namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff.Panel
open SpectreCoff.Layout

type PanelSettings()  =
    inherit CommandSettings()

type PanelExample() =
    inherit Command<PanelSettings>()
    interface ICommandLimiter<PanelSettings>

    override _.Execute(_context, _) = 
        let examplePanel = panel "First !" "This is my first panel content" 
        examplePanel |> toConsole    

        let custom = customPanel { defaultPanelLayout with Sizing = Expand } "Custom !!" "This is my first panel content" 
        custom |> toConsole
        0

open SpectreCoff.Cli

type PanelDocumentation() =
    inherit Command<PanelSettings>()
    interface ICommandLimiter<PanelSettings>
    
    override _.Execute(_context, _) = 
        Theme.setDocumentationStyle
        0
