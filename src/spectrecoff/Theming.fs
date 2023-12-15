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
    | Volcano
    | CyberPunk
    | NeonLights
    | FuturisticBlue
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
          Decorations = [ Decoration.Bold ] } }

let volcanoTheme =
    { calmLook =
        { Color = Some Color.DarkRed_1
          BackgroundColor = None
          Decorations = [ Decoration.None ] }
      pumpedLook =
        { Color = Some Color.Orange3
          BackgroundColor = None
          Decorations = [ Decoration.Italic ] }
      edgyLook =
        { Color = Some Color.Yellow3_1
          BackgroundColor = None
          Decorations = [ Decoration.Italic; Decoration.Bold ] } }

let cyberPunkTheme =
    { calmLook =
        { Color = Some Color.DarkCyan
          BackgroundColor = None
          Decorations = [ Decoration.None ] }
      pumpedLook =
        { Color = Some Color.DeepSkyBlue2
          BackgroundColor = None
          Decorations = [ Decoration.Italic ] }
      edgyLook =
        { Color = Some Color.DarkMagenta
          BackgroundColor = None
          Decorations = [ Decoration.Italic; Decoration.Bold ] } }

let neonLightsTheme =
    { calmLook =
        { Color = Some Color.Lime
          BackgroundColor = None
          Decorations = [ Decoration.None ] }
      pumpedLook =
        { Color = Some Color.Chartreuse2
          BackgroundColor = None
          Decorations = [ Decoration.Italic ] }
      edgyLook =
        { Color = Some Color.OrangeRed1
          BackgroundColor = None
          Decorations = [ Decoration.Italic; Decoration.Bold ] } }

let futuristicBlueTheme =
    { calmLook =
        { Color = Some Color.DodgerBlue1
          BackgroundColor = None
          Decorations = [ Decoration.None ] }
      pumpedLook =
        { Color = Some Color.DeepSkyBlue1
          BackgroundColor = None
          Decorations = [ Decoration.Italic ] }
      edgyLook =
        { Color = Some Color.SteelBlue1
          BackgroundColor = None
          Decorations = [ Decoration.Italic; Decoration.Bold ] } }

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

let applyTheme theme =
    calmLook <- theme.calmLook
    pumpedLook <- theme.pumpedLook
    edgyLook <- theme.edgyLook

let selectTheme spectreCoffTheme =
    match spectreCoffTheme with
    | Iceberg -> iceBergTheme
    | Volcano -> volcanoTheme
    | CyberPunk -> cyberPunkTheme
    | NeonLights -> neonLightsTheme
    | FuturisticBlue -> futuristicBlueTheme
    | Documentation -> documentationTheme
    |> applyTheme