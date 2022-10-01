module SpectreFs.Output
open Spectre.Console

let mutable standardStyle = "blue"
let mutable emphasizeStyle = "green"
let mutable warningStyle = "red"

let private markup style content = $"[{style}]{Markup.Escape content}[/]"

let emphasize content = markup emphasizeStyle content
let warn content = markup warningStyle content
let standard content = markup standardStyle content

let printMarkedUp content = AnsiConsole.Markup $"{content}"
let printMarkedUpNL content = AnsiConsole.Markup $"{content}{System.Environment.NewLine}"

type MarkedUpString = 
| Standard of string
| StandardLine of string
| Emphasized of string
| EmphasizedLine of string
| Warning of string
| WarningLine of string

let putMarkedUp (parts: seq<MarkedUpString>) = 
    for part in parts do
        match part with
        | Standard s -> printMarkedUp (standard s)
        | Emphasized s -> printMarkedUp (emphasize s) 
        | Warning s -> printMarkedUp (warn s)
        | StandardLine s -> printMarkedUpNL (standard s)
        | EmphasizedLine s -> printMarkedUpNL (emphasize s)
        | WarningLine s -> printMarkedUpNL (warn s)

let rec toMarkedUpString (parts: seq<obj>) = 
    parts
    |> Seq.map (fun part -> 
        match part with
        | :? MarkedUpString as ms->  seq { ms }
        | :? seq<obj> as lineParts -> toMarkedUpString lineParts
        | other -> seq { Standard (other.ToString()) })
    |> Seq.collect (fun part -> part)

let rec put (parts: seq<obj>) =
    parts
    |> toMarkedUpString
    |> putMarkedUp

let putSeparatedBy separator (parts: obj list) = 
    Seq.initInfinite (fun _ -> separator)
    |> Seq.zip parts 
    |> Seq.map (fun pair -> [fst pair; snd pair])
    |> Seq.collect (fun value -> value)
    |> put 

let putLines parts =
    putSeparatedBy System.Environment.NewLine parts