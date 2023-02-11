[<AutoOpen>]
module SpectreCoff.Json

open Spectre.Console
open Spectre.Console.Json
open SpectreCoff.Output

let mutable bracesColor = calmColor
let mutable bracesDecorations = [ Decoration.None ]

let mutable bracketsColor = calmColor
let mutable bracketsDecorations = [ Decoration.None ]

let mutable colonColor = calmColor
let mutable colonDecorations = [ Decoration.None ]

let mutable commaColor = calmColor
let mutable commaDecorations = [ Decoration.None ]

let mutable memberColor = pumpedColor
let mutable memberDecorations = [ Decoration.Italic ]

let mutable stringColor = edgyColor
let mutable stringDecorations = [ Decoration.Bold ]

let mutable numberColor = edgyColor
let mutable numberDecorations = [ Decoration.Bold ]

let mutable booleanColor = calmColor
let mutable booleanDecorations = [ Decoration.Bold ]

let mutable nullColor = calmColor
let mutable nullDecorations = [ Decoration.Dim; Decoration.Strikethrough ]

let private aggregate decorations =
    decorations |>  List.reduce (|||)

let private applyColors (json: JsonText) =
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
    |> applyColors
    :> Rendering.IRenderable
    |> Renderable
