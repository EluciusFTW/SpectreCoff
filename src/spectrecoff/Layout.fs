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
      Color: Color }

let private aggregate decorations =
    decorations |>  List.reduce (|||)

let toStyle look = 
    Style (look.Color, System.Nullable(), aggregate look.Decorations)