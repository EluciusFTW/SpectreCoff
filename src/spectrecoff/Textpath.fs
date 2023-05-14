[<AutoOpen>]
module SpectreCoff.Textpath

open Spectre.Console
open SpectreCoff.Layout
open SpectreCoff.Output

let mutable stemColor = calmLook.Color
let mutable rootColor = calmLook.Color
let mutable separatorColor = pumpedLook.Color
let mutable leafColor = edgyLook.Color

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
