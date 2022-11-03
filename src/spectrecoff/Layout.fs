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

type Style = 
    | Bold
    | Dim
    | Italic
    | Underline
    | Invert
    | Conceal
    | Slowblink
    | Rapidblink
    | StrikeThrough

let stringifyStyle style = 
    match style with
    | Bold -> "bold"
    | Dim -> "dim"
    | Italic -> "italic"
    | Underline -> "underline"
    | Invert -> "invert"
    | Conceal -> "conceal"
    | Slowblink -> "slowblink"
    | Rapidblink -> "rapidblink"
    | StrikeThrough -> "strikethrough"