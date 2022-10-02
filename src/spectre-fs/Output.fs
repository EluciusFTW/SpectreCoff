module SpectreFs.Output
open Spectre.Console

// Styles
let mutable standardStyle = "blue"
let mutable emphasizeStyle = "green"
let mutable warningStyle = "red"

// Special strings
let mutable bulletItemPrefix = " + "

// Basic output
let private markup style content = $"[{style}]{Markup.Escape content}[/]"

let emphasize content = markup emphasizeStyle content
let warn content = markup warningStyle content
let standard content = markup standardStyle content

let printMarkedUp content = AnsiConsole.Markup $"{content}"
let printMarkedUpNL content = AnsiConsole.Markup $"{content}{System.Environment.NewLine}"

// -------------- MarkedUp Version 1 ---------------
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

// ---------------- 2nd Version ----------
type complexOutput = 
| S of string
| E of string
| W of string
| NL | SP 
| MU of MarkedUpString
| CO of complexOutput list
| BI of complexOutput list
| OB of obj

let rec toMarkedUp (parts: seq<complexOutput>) = 
    parts
    |> Seq.map (fun part -> 
        match part with
        | S s -> seq { Standard s }
        | E s -> seq { Emphasized s }
        | W s -> seq { Warning s }
        | NL -> seq { Standard System.Environment.NewLine }
        | SP -> seq { Standard " " }
        | MU ms->  seq { ms }
        | CO items -> toMarkedUp items
        | BI items -> 
            items 
            |> List.map (fun item -> CO [S bulletItemPrefix; item; NL])
            |> toMarkedUp
        | OB other -> seq { Standard (other.ToString()) })
    |> Seq.collect (fun part -> part)

let rec putComplex (parts: seq<complexOutput>) =
    parts
    |> toMarkedUp
    |> putMarkedUp

let putComplexSeparatedBy separator (parts: complexOutput list) = 
    Seq.initInfinite (fun _ -> separator)
    |> Seq.zip parts 
    |> Seq.map (fun pair -> [fst pair; snd pair])
    |> Seq.collect (fun value -> value)
    |> putComplex 

let putComplexWords (parts: complexOutput list) =
    putComplexSeparatedBy (S " ") parts

let putComplexThings (parts: complexOutput list) =
    putComplex parts

let putComplexLines (parts: complexOutput list) =
    putComplexSeparatedBy (S System.Environment.NewLine) parts

let putStandardLines (parts: string list) =
    putComplexSeparatedBy (S System.Environment.NewLine) (parts |> List.map S)