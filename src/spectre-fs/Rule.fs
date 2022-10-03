module SpectreFs.Rule
open Spectre.Console

type Alignment =
    | Left
    | Center
    | Right

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
