[<AutoOpen>]
module SpectreCoff.Figlet

open Spectre.Console 
open SpectreCoff.Styling
open SpectreCoff.Output

let mutable defaultAlignment = Center
let mutable defaultColor = pumpedLook.Color
let mutable defaultLayoutMode = FigletLayoutMode.FullSize

let private applyAlignment alignment figlet = 
    match alignment with
    | Left -> figlet.LeftJustified() |> ignore
    | Center -> figlet.Centered() |> ignore
    | Right -> figlet.RightJustified() |> ignore
    figlet

let private toRenderable figlet = 
    figlet
    :> Rendering.IRenderable
    |> Renderable

let private applyColor (colorOption: Color Option) (figlet: FigletText) = 
    match colorOption with 
    | Some color ->  figlet.Color <- color
    | None -> ()
    figlet

let private applyLayoutMode mode (figlet: FigletText) =
    figlet.LayoutMode <- mode
    figlet

let customFigletWithMode mode (alignment: Alignment) (color: Color) content = 
    FigletText content
    |> applyColor (Some color)
    |> applyAlignment alignment
    |> applyLayoutMode mode
    |> toRenderable

let customFiglet (alignment: Alignment) (color: Color) content = 
    customFigletWithMode defaultLayoutMode alignment color content

let figlet content = 
    FigletText content
    |> applyColor defaultColor
    |> applyAlignment defaultAlignment
    |> applyLayoutMode defaultLayoutMode
    |> toRenderable 