[<AutoOpen>]
module SpectreCoff.Figlet

open Spectre.Console 
open SpectreCoff.Layout
open SpectreCoff.Output

let mutable defaultAlignment = Center
let mutable defaultColor = pumpedColor

let customFiglet (alignment: Alignment) (color: Color) content = 
    let figlet = FigletText content
    figlet.Color <- color

    match alignment with
    | Left -> figlet.LeftAligned() |> ignore
    | Center -> figlet.Centered() |> ignore
    | Right -> figlet.RightAligned() |> ignore

    figlet
    :> Rendering.IRenderable
    |> Renderable

let figlet = 
    customFiglet defaultAlignment defaultColor 