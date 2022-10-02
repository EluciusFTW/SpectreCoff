module SpectreFs.Output
open Spectre.Console

let mutable emphasizeStyle = "green"
let mutable warningStyle = "red"

let private markup style content = $"[{style}]{content}[/]"

let emphasize content = markup emphasizeStyle content
let warn content = markup warningStyle content

let printMarkedUp content = AnsiConsole.Markup $"{content}{System.Environment.NewLine}"
