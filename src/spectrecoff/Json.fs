[<AutoOpen>]
module SpectreCoff.Json

open Spectre.Console
open SpectreCoff.Layout
open SpectreCoff.Output

let mutable bracesColor = calmColor
let mutable bracketColor = calmColor
let mutable colonColor = pumpedColor
let mutable commaColor = edgyColor

let mutable stringColor = calmColor
let mutable numberColor = calmColor
let mutable booleanColor = pumpedColor
let mutable nullColor = edgyColor

// let private applyColors (path: TextPath) =
//     path.LeafColor leafColor |> ignore
//     path.SeparatorColor separatorColor |> ignore
//     path.StemColor stemColor |> ignore
//     path.RootColor rootColor

// let alignedPath alignment value =
//     let path = TextPath value

//     match alignment with
//     | Left -> path.LeftJustified() |> ignore
//     | Center -> path.Centered() |> ignore
//     | Right -> path.RightJustified() |> ignore

//     path 
//     |> applyColors 
//     :> Rendering.IRenderable
//     |> Renderable

let json content = 
    Spectre.Console.JsonText(content)
    :> Rendering.IRenderable
    |> Renderable
