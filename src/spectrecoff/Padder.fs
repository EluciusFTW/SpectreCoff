[<AutoOpen>]
module SpectreCoff.Padder

open Spectre.Console
open SpectreCoff.Output

let pad top right bottom left element = 
    // We follow the Css convention of argument order: Start at top, then clockwise.
    // Spectre.Console also goes clockwise, but starts left.
    let padder = Padder(element |> payloadToRenderable, (Padding(left, top, right, bottom)))
    padder
    :> Rendering.IRenderable
    |> Renderable

let padtop top = 
    pad top 0 0 0 

let padbottom bottom =
    pad 0 0 bottom 0 

let padright right =
    pad 0 right 0 0 

let padleft left =
    pad 0 0 0 left

let padHorizontal amount =
    pad 0 amount 0 amount

let padVertical amount =
    pad amount 0 amount 0  

let padSymmetric horizontal vertical = 
    pad vertical horizontal vertical horizontal
