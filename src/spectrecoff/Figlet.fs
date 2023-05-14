[<AutoOpen>]
module SpectreCoff.Figlet

open Spectre.Console 
open SpectreCoff.Layout
open SpectreCoff.Output

let mutable defaultAlignment = Center
let mutable defaultColor = pumpedLook.Color

let customFiglet (alignment: Alignment) (color: Color) content = 
    let figlet = FigletText content
    figlet.Color <- color

    match alignment with
    | Left -> figlet.LeftJustified() |> ignore
    | Center -> figlet.Centered() |> ignore
    | Right -> figlet.RightJustified() |> ignore

    figlet
    :> Rendering.IRenderable
    |> Renderable

let figlet = 
    customFiglet defaultAlignment defaultColor 