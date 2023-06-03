[<AutoOpen>]
module SpectreCoff.Panel

open Spectre.Console
open SpectreCoff.Layout
open SpectreCoff.Output

type PanelLayout =
    { Border: BoxBorder;
      BorderColor: Color Option;
      Sizing: SizingBehaviour;
      Padding: Padding }

let mutable defaultPanelLayout: PanelLayout =
    { Border = BoxBorder.Rounded
      BorderColor = edgyLook.Color
      Sizing = Collapse
      Padding = AllEqual 2 }

let private addHeader header (panel: Panel) = 
   panel.Header <- PanelHeader(header)
   panel

let private addBorder border (panel: Panel) = 
   panel.Border <- border
   panel

let private applyLayout (layout: PanelLayout) (panel: Panel) = 
    match layout.BorderColor with
    | Some color -> panel.BorderColor color |> ignore
    | None -> ()

    match layout.Sizing with
    | Expand -> panel.Expand <- true
    | Collapse -> panel.Collapse() |> ignore

    match layout.Padding with
    | AllEqual value -> panel.Padding <- Spectre.Console.Padding(value)
    | HorizontalVertical (hor,vert) -> panel.Padding <- Spectre.Console.Padding(hor, vert)
    | TopRightBottomLeft (l,t,r,b) -> panel.Padding <- Spectre.Console.Padding(l, t, r, b)
    panel  

let private toRenderable panel = 
    panel
    :> Rendering.IRenderable
    |> Renderable

let customPanel (layout: PanelLayout) (header: string) (content: OutputPayload) =
    Panel(content |> payloadToRenderable)
    |> addHeader header
    |> addBorder layout.Border
    |> applyLayout layout
    |> toRenderable

let panel =
   customPanel defaultPanelLayout