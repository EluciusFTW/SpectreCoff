[<AutoOpen>]
module SpectreCoff.Styling

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
      Color: Color Option;
      BackgroundColor: Color Option }

let private aggregate decorations =
    decorations |>  List.reduce (|||)

let private toNullable colorOption = 
    match colorOption with 
    | Some color -> System.Nullable<Color> color
    | None -> System.Nullable()

let toSpectreStyle look =
    Style (toNullable look.Color, toNullable look.BackgroundColor, aggregate look.Decorations)