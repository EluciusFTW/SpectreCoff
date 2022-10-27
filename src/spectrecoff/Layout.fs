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
    | Standard
    | Bold
    | Dim
    | Italic
    | Underline
    | Invert
    | Conceal
    | Slowblink
    | Rapidblink
    | StrikeThrough
    | Link

let stringifyStyle style = 
    match style with
    | Standard -> ""
    | Bold -> "bold"
    | Dim -> "dim"
    | Italic -> "italic"
    | Underline -> "underline"
    | Invert -> "invert"
    | Conceal -> "conceal"
    | Slowblink -> "slowblink"
    | Rapidblink -> "rapidblink"
    | StrikeThrough -> "strikethrough"
    | Link -> "link"