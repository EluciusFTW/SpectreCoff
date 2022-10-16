module SpectreCoff.Rule
open SpectreCoff.Layout
open Spectre.Console

let mutable defaultAlignment = Center

let emptyRule =
    Rule()
    |> AnsiConsole.Write

let alignedRule alignment content =
    match alignment with
    | Left -> Rule(content).LeftAligned()
    | Center -> Rule(content)
    | Right -> Rule(content).RightAligned()
    |> AnsiConsole.Write

let rule content =
    alignedRule defaultAlignment content
