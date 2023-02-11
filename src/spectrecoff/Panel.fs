[<AutoOpen>]
module SpectreCoff.Panel

open Spectre.Console
open SpectreCoff.Layout
open SpectreCoff.Output

type PanelLayout =
    { Border: BoxBorder;
      BorderColor: Color;
      Sizing: SizingBehaviour;
      Padding: Padding }

let mutable defaultPanelLayout: PanelLayout =
    { Border = BoxBorder.Rounded
      BorderColor = edgyColor
      Sizing = Collapse
      Padding = AllEqual 2 }

let customPanel (layout: PanelLayout) (header: string) (content: OutputPayload) =
    let panel = Panel(content |> payloadToRenderable)
    panel.Header <- PanelHeader(header)

    panel.Border <- layout.Border
    panel.BorderColor layout.BorderColor |> ignore

    match layout.Sizing with
    | Expand -> panel.Expand <- true
    | Collapse -> panel.Collapse() |> ignore

    match layout.Padding with
    | AllEqual value -> panel.Padding <- Spectre.Console.Padding(value)
    | HorizontalVertical (hor,vert) -> panel.Padding <- Spectre.Console.Padding(hor, vert)
    | TopRightBottomLeft (l,t,r,b) -> panel.Padding <- Spectre.Console.Padding(l, t, r, b)

    panel
    :> Rendering.IRenderable
    |> Renderable

let panel =
   customPanel defaultPanelLayout