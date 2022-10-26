module SpectreCoff.Panel

open Spectre.Console
open SpectreCoff.Layout

type PanelLayout =
    {  Border: BoxBorder;
       Sizing: SizingBehaviour;
       Padding: Padding }

let defaultPanelLayout: PanelLayout =
    {  Border = BoxBorder.Heavy
       Sizing = Collapse
       Padding = AllEqual 2 }

let customPanel (layout: PanelLayout) (header: string) (content: string) =
    let panel = Panel(content)
    panel.Header <- PanelHeader(header)

    match layout.Sizing with
    | Expand -> panel.Expand <- true
    | Collapse -> panel.Collapse() |> ignore

    match layout.Padding with
    | AllEqual value -> panel.Padding <- Spectre.Console.Padding(value)
    | HorizontalVertical (hor,vert) -> panel.Padding <- Spectre.Console.Padding(hor, vert)
    | TopRightBottomLeft (l,t,r,b) -> panel.Padding <- Spectre.Console.Padding(l, t, r, b)

    panel

let panel =
   customPanel defaultPanelLayout

let toConsole (panel: Panel) =
    panel |> AnsiConsole.Write