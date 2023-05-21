[<AutoOpen>]
module SpectreCoff.Json

open Spectre.Console
open Spectre.Console.Json
open SpectreCoff.Output

let mutable bracesLook = 
    { Color = calmLook.Color
      BackgroundColor = None
      Decorations = [ Decoration.None ] }

let mutable bracketsLook = 
    { Color = calmLook.Color
      BackgroundColor = None
      Decorations = [ Decoration.None ] }

let mutable colonLook = 
    { Color = calmLook.Color
      BackgroundColor = None
      Decorations = [ Decoration.None ] }

let mutable commaLook = 
    { Color = calmLook.Color
      BackgroundColor = None
      Decorations = [ Decoration.None ] }

let mutable memberLook = 
    { Color = pumpedLook.Color
      BackgroundColor = None
      Decorations = [ Decoration.Italic ] }

let mutable stringLook = 
    { Color = edgyLook.Color
      BackgroundColor = None
      Decorations = [ Decoration.Bold ] }

let mutable numberLook = 
    { Color = edgyLook.Color
      BackgroundColor = None
      Decorations = [ Decoration.Bold ] }

let mutable booleanLook = 
    { Color = calmLook.Color
      BackgroundColor = None
      Decorations = [ Decoration.Bold ] }

let mutable nullLook = 
    { Color = calmLook.Color
      BackgroundColor = None
      Decorations = [ Decoration.Dim ] }

let private applyStyles (json: JsonText) =
    json.BracesStyle <- toSpectreStyle bracesLook
    json.BracketsStyle <- toSpectreStyle bracketsLook
    json.ColonStyle <- toSpectreStyle colonLook
    json.CommaStyle <- toSpectreStyle commaLook
    json.StringStyle <- toSpectreStyle stringLook
    json.NumberStyle <- toSpectreStyle numberLook
    json.BooleanStyle <- toSpectreStyle booleanLook
    json.MemberStyle <- toSpectreStyle memberLook
    json.NullStyle <- toSpectreStyle nullLook
    json

let json content = 
    JsonText content
    |> applyStyles
    :> Rendering.IRenderable
    |> Renderable
