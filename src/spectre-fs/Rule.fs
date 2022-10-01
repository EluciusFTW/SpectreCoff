module SpectreFs.Rule
open Spectre.Console

let ruleEmpty =
    AnsiConsole.Write(Rule())

let ruleContent content =
    AnsiConsole.Write(Rule(content))

let ruleContentLeft content =
    let rule = Rule(content).LeftAligned()
    AnsiConsole.Write(rule)

let ruleContentRight content =
    let rule = Rule(content).RightAligned()
    AnsiConsole.Write(rule)