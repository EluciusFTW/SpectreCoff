[<AutoOpen>]
module SpectreCoff.Layout

type Alignment =
    | Left
    | Center
    | Right

type SizingBehaviour =
    | Expand
    | Collapse

type Padding =
    | AllEqual of int
    | HorizontalVertical of int*int
    | TopRightBottomLeft of int*int*int*int

open Spectre.Console

type Look = 
    { Decorations: Decoration list;
      Color: Color;
      BackgroundColor: Color Option }

let private aggregate decorations =
    decorations |>  List.reduce (|||)

let toSpectreStyle look =
    let backgroundColor = 
        match look.BackgroundColor with
        | Some color -> System.Nullable<Color> color
        | None -> System.Nullable()

    Style (look.Color, backgroundColor, aggregate look.Decorations)