module SpectreCoff.Panel

open Spectre.Console
open SpectreCoff.Layout
open Output

type PanelLayout =
    { Border: BoxBorder
      Sizing: SizingBehaviour
      Padding: Padding }

let defaultPanelLayout: PanelLayout =
    { Border = BoxBorder.Heavy
      Sizing = Collapse
      Padding = AllEqual 2 }

let alterLayout (layout: PanelLayout) (panel: Panel) =
    match layout.Sizing with
    | Expand -> panel.Expand <- true
    | Collapse -> panel.Collapse() |> ignore

    match layout.Padding with
    | AllEqual value -> panel.Padding <- Spectre.Console.Padding(value)
    | HorizontalVertical (hor, vert) -> panel.Padding <- Spectre.Console.Padding(hor, vert)
    | TopRightBottomLeft (l, t, r, b) -> panel.Padding <- Spectre.Console.Padding(l, t, r, b)

    panel

let customPanel (layout: PanelLayout) (header: string) (content: string) =
    let panel = Panel(content)
    panel.Header <- PanelHeader(header)
    panel |> alterLayout layout

let cP (layout: PanelLayout) (h: OutputPayload) (c: OutputPayload) =
    let panel = Panel(c |> payloadToRenderable)
    panel.Header <- PanelHeader(h |> toMarkedUpString)

    panel 
    |> alterLayout layout 
    :> Rendering.IRenderable
    |> Ren

let vP = cP defaultPanelLayout

let vanillaPanel = customPanel defaultPanelLayout
