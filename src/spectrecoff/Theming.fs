[<AutoOpen>]
module SpectreCoff.Theming

open SpectreCoff
open Spectre.Console

type theme = 
    { calmLook: Look
      pumpedLook: Look
      edgyLook: Look }

type SpectreCoffThemes = 
    | Iceberg
    | Documentation

let iceBergTheme = 
    { calmLook = 
        { Color = Some Color.SteelBlue
          BackgroundColor = None
          Decorations = [ Decoration.None ] }
      pumpedLook = 
        { Color = Some Color.DeepSkyBlue3_1
          BackgroundColor = None
          Decorations = [ Decoration.Italic ] }
      edgyLook = 
        { Color = Some Color.DarkTurquoise
          BackgroundColor = None
          Decorations = [ Decoration.Bold ] }
    }

let documentationTheme = 
    { iceBergTheme with 
        pumpedLook = 
            { Color = Some Color.Yellow
              BackgroundColor = None
              Decorations = [ Decoration.Italic ] }
        edgyLook =
            { Color = Some Color.DarkKhaki
              BackgroundColor = Some Color.OrangeRed1
              Decorations = [ Decoration.Italic ] }}

let themes = [
    iceBergTheme
    documentationTheme
]

let applyTheme theme = 
    calmLook <- theme.calmLook 
    pumpedLook <- theme.pumpedLook 
    edgyLook <- theme.edgyLook 

let selectTheme spectreCoffTheme = 
    match spectreCoffTheme with
    | Iceberg -> iceBergTheme
    | Documentation -> documentationTheme
    |> applyTheme