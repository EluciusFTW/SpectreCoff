[<AutoOpen>]
module SpectreCoff.Textpath

open Spectre.Console
open SpectreCoff.Layout
open SpectreCoff.Output

let mutable stemLook = calmLook
let mutable rootLook = calmLook
let mutable separatorLook = pumpedLook
let mutable leafLook = edgyLook

let mutable defaultAlignment = Left

let private applyLooks (path: TextPath) =
    path.LeafStyle <- toSpectreStyle leafLook
    path.SeparatorStyle <- toSpectreStyle separatorLook
    path.RootStyle <- toSpectreStyle rootLook
    path.StemStyle <- toSpectreStyle stemLook
    path

let private applyAlignment alignment path =
    match alignment with
    | Left -> path.LeftJustified() |> ignore
    | Center -> path.Centered() |> ignore
    | Right -> path.RightJustified() |> ignore
    path

let private toRenderable path = 
    path
    :> Rendering.IRenderable
    |> Renderable

let alignedPath alignment value =
    TextPath value
    |> applyAlignment alignment 
    |> applyLooks 
    |> toRenderable

let path = 
    alignedPath defaultAlignment
