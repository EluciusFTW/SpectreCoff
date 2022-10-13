module SpecteCoff.Output
open Spectre.Console

// Styles
let mutable standardStyle = ""
let mutable standardColor = "blue"

let mutable emphasizeStyle = "bold"
let mutable emphasizeColor = "lightpink4"

let mutable warningStyle = "italic"
let mutable warningColor = "red"

// Special strings
let mutable bulletItemPrefix = " + "

// Basic output
let markup color style content = 
    match style with
    | "" -> $"[{color}]{Markup.Escape content}[/]"
    | _ -> $"[{color} {style}]{Markup.Escape content}[/]"

let emphasize content = markup emphasizeColor emphasizeStyle content
let warn content = markup warningColor warningStyle content
let standard content = markup standardColor standardStyle content

let printMarkedUp content = AnsiConsole.Markup $"{content}"
let printMarkedUpNL content = AnsiConsole.Markup $"{content}{System.Environment.NewLine}"

type PrintPayload = 
    | S of string
    | Standard of string
    | E of string
    | Emphasize of string
    | W of string
    | Warn of string
    | C of string
    | Custom of string
    | CO of PrintPayload list
    | Collection of PrintPayload list
    | BI of PrintPayload list
    | BulletItems of PrintPayload list
    | NewLine    
    | Many of string list
    | ManyMarkedUp of PrintPayload list

let printInline (payload: PrintPayload) = 
    match payload with
    | S value
    | Standard value ->  printMarkedUp (standard value)
    | E value 
    | Emphasize value -> printMarkedUp (emphasize value)
    | W value
    | Warn value -> printMarkedUp (warn value)
    | C value
    | Custom value -> printMarkedUp value
    | _ -> ()

let rec toConsole (payload: PrintPayload) = 
    match payload with
    | S value
    | Standard value ->  printMarkedUpNL (standard value)
    | E value
    | Emphasize value -> printMarkedUpNL (emphasize value)
    | W value
    | Warn value -> printMarkedUpNL (warn value)
    | C value
    | Custom value -> printMarkedUpNL value
    | NewLine -> printfn ""
    | CO items
    | Collection items -> 
        items |> List.iter printInline
        printfn ""
    | BI items
    | BulletItems items -> 
        items 
        |> List.map (fun item -> CO [S bulletItemPrefix; item])
        |> List.iter toConsole
    | Many values -> values |> List.iter (fun value -> printMarkedUpNL (standard value))
    | ManyMarkedUp markedUp -> markedUp |> List.iter toConsole