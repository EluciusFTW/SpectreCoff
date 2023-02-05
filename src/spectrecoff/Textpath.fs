[<AutoOpen>]
module SpectreCoff.Textpath

open Spectre.Console
open SpectreCoff.Layout
open SpectreCoff.Output

let mutable stemColor = calmColor
let mutable rootColor = calmColor
let mutable separatorColor = pumpedColor
let mutable leafColor = edgyColor

let mutable defaultAlignment = Left

let private applyColors (path: TextPath) =
    path.LeafColor leafColor |> ignore
    path.SeparatorColor separatorColor |> ignore
    path.StemColor stemColor |> ignore
    path.RootColor rootColor

let alignedPath alignment value =
    let path = TextPath value

    match alignment with
    | Left -> path.LeftJustified() |> ignore
    | Center -> path.Centered() |> ignore
    | Right -> path.RightJustified() |> ignore

    path 
    |> applyColors 
    :> Rendering.IRenderable
    |> Renderable

let path = 
    alignedPath defaultAlignment
