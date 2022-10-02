module SpectreFs.Rule
open Spectre.Console

let ruleEmpty =
    Rule()
    |> AnsiConsole.Write

let ruleContent content =
    content
    |> Rule
    |> AnsiConsole.Write

let ruleContentLeft content =
    Rule(content).LeftAligned()
    |> AnsiConsole.Write

let ruleContentRight content =
    Rule(content).RightAligned()
    |> AnsiConsole.Write