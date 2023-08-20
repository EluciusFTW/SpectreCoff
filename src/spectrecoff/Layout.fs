[<AutoOpen>]
module SpectreCoff.Layout

open Spectre.Console

let splitHorizontally children (layout: Layout) =
    layout.SplitRows children

let splitVertically children (layout: Layout) =
    layout.SplitColumns children

let setChildContent identifier content (layout: Layout) =
    layout[identifier].Update content |> ignore
    layout

let setContent content (layout: Layout) =
    layout.Update content |> ignore
    layout

let withChildMinimumWidth identifier width (layout: Layout) =
    layout[identifier].MinimumSize <- width
    layout

let withMinimumWidth width (layout: Layout) =
    layout.MinimumSize <- width
    layout

let withChildExplicitWidth identifier width (layout: Layout) =
    layout[identifier].Size <- width
    layout

let withExplicitWidth width (layout: Layout) =
    layout.Size <- width
    layout

let withChildRatio identifier ratio (layout: Layout) =
    layout[identifier].Ratio <- ratio
    layout

let withRatio ratio (layout: Layout) =
    layout.Ratio <- ratio
    layout

let showChild identifier (layout: Layout) =
    layout[identifier].Visible()

let show (layout: Layout) =
    layout.Visible()

let hideChild identifier (layout: Layout) =
    layout[identifier].Invisible()

let hide layout =
    layout.Invisible()

let layout (identifier: string) =
    Layout identifier