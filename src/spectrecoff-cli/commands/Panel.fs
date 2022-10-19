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
        let examplePanel = panel "First !" "Important text can be highlighted by surrounding it with a border and title." 
        examplePanel |> toConsole    

        let custom = customPanel { defaultPanelLayout with Sizing = Expand } "Custom !!" "That surrounding border can be customized easily, e.g., to take up as much horizontal space as needed." 
        custom |> toConsole
        0

open SpectreCoff.Cli

type PanelDocumentation() =
    inherit Command<PanelSettings>()
    interface ICommandLimiter<PanelSettings>
    
    override _.Execute(_context, _) = 
        Theme.setDocumentationStyle
        0
