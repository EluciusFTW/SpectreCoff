[<AutoOpen>]
module SpectreCoff.Json

open Spectre.Console
open Spectre.Console.Json
open SpectreCoff.Output

let mutable bracesLook = 
    { Color = calmLook.Color
      Decorations = [ Decoration.None ] }

let mutable bracketsLook = 
    { Color = calmLook.Color
      Decorations = [ Decoration.None ] }

let mutable colonLook = 
    { Color = calmLook.Color
      Decorations = [ Decoration.None ] }

let mutable commaLook = 
    { Color = calmLook.Color
      Decorations = [ Decoration.None ] }

let mutable memberLook = 
    { Color = pumpedLook.Color
      Decorations = [ Decoration.Italic ] }

let mutable stringLook = 
    { Color = edgyLook.Color
      Decorations = [ Decoration.Bold ] }

let mutable numberLook = 
    { Color = edgyLook.Color
      Decorations = [ Decoration.Bold ] }

let mutable booleanLook = 
    { Color = calmLook.Color
      Decorations = [ Decoration.Bold ] }

let mutable nullLook = 
    { Color = calmLook.Color
      Decorations = [ Decoration.Dim ] }


let private aggregate decorations =
    decorations |>  List.reduce (|||)

let private applyStyles (json: JsonText) =
    json.BracesStyle <- toStyle bracesLook
    json.BracketsStyle <- toStyle bracketsLook
    json.ColonStyle <- toStyle colonLook
    json.CommaStyle <- toStyle commaLook
    json.StringStyle <- toStyle stringLook
    json.NumberStyle <- toStyle numberLook
    json.BooleanStyle <- toStyle booleanLook
    json.MemberStyle <- toStyle memberLook
    json.NullStyle <- toStyle nullLook
    json

let json content = 
    JsonText content
    |> applyStyles
    :> Rendering.IRenderable
    |> Renderable
