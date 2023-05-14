[<AutoOpen>]
module SpectreCoff.Json

open Spectre.Console
open Spectre.Console.Json
open SpectreCoff.Output

let mutable bracesColor = calmLook.Color
let mutable bracesDecorations = [ Decoration.None ]

let mutable bracketsColor = calmLook.Color
let mutable bracketsDecorations = [ Decoration.None ]

let mutable colonColor = calmLook.Color
let mutable colonDecorations = [ Decoration.None ]

let mutable commaColor = calmLook.Color
let mutable commaDecorations = [ Decoration.None ]

let mutable memberColor = pumpedLook.Color
let mutable memberDecorations = [ Decoration.Italic ]

let mutable stringColor = edgyLook.Color
let mutable stringDecorations = [ Decoration.Bold ]

let mutable numberColor = edgyLook.Color
let mutable numberDecorations = [ Decoration.Bold ]

let mutable booleanColor = calmLook.Color
let mutable booleanDecorations = [ Decoration.Bold ]

let mutable nullColor = calmLook.Color
let mutable nullDecorations = [ Decoration.Dim ]

let private aggregate decorations =
    decorations |>  List.reduce (|||)

let private applyStyles (json: JsonText) =
    json.BracesStyle <- Style (bracesColor, System.Nullable(), aggregate bracesDecorations)
    json.BracketsStyle <- Style (bracketsColor, System.Nullable(), aggregate bracesDecorations)
    json.ColonStyle <- Style (colonColor, System.Nullable(), aggregate colonDecorations)
    json.CommaStyle <- Style (commaColor, System.Nullable(), aggregate commaDecorations)
    json.StringStyle <- Style (stringColor, System.Nullable(), aggregate stringDecorations)
    json.NumberStyle <- Style (numberColor, System.Nullable(), aggregate numberDecorations)
    json.BooleanStyle <- Style (booleanColor, System.Nullable(), aggregate booleanDecorations)
    json.MemberStyle <- Style (memberColor, System.Nullable(), aggregate memberDecorations)
    json.NullStyle <- Style (nullColor, System.Nullable(), aggregate nullDecorations)
    json

let json content = 
    JsonText content
    |> applyStyles
    :> Rendering.IRenderable
    |> Renderable
