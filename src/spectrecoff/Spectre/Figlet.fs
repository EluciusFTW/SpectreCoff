[<AutoOpen>]
module SpectreCoff.Figlet

open Spectre.Console 
open SpectreCoff.Styling
open SpectreCoff.Output

let mutable defaultAlignment = Center
let mutable defaultColor = pumpedLook.Color

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

let customFiglet (alignment: Alignment) (color: Color) content = 
    FigletText content
    |> applyColor (Some color)
    |> applyAlignment alignment
    |> toRenderable

let figlet content = 
    FigletText content
    |> applyColor defaultColor
    |> applyAlignment defaultAlignment
    |> toRenderable 