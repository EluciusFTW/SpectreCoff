module SpectreCoff.Output
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
    | _ -> 
        match color with
        | "" -> $"[{style}]{Markup.Escape content}[/]"
        | _ ->  $"[{color} {style}]{Markup.Escape content}[/]"

let emphasize content = markup emphasizeColor emphasizeStyle content
let warn content = markup warningColor warningStyle content
let standard content = markup standardColor standardStyle content

let printMarkedUp content = AnsiConsole.Markup $"{content}"
let printMarkedUpNL content = AnsiConsole.Markup $"{content}{System.Environment.NewLine}"

type OutputPayload = 
    | MCS of string*string*string
    | MarkupCS of string*string*string
    | MC of string*string
    | MarkupC of string*string
    | MS of string*string
    | MarkupS of string*string
    | S of string
    | Standard of string
    | E of string
    | Emphasize of string
    | W of string
    | Warn of string
    | C of string
    | Custom of string
    | CO of OutputPayload list
    | Collection of OutputPayload list
    | BI of OutputPayload list
    | BulletItems of OutputPayload list
    | NewLine    
    | Many of string list
    | ManyMarkedUp of OutputPayload list

let printInline (payload: OutputPayload) = 
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

let rec toConsole (payload: OutputPayload) = 
    match payload with
    | MCS (color, style, content)
    | MarkupCS (color, style, content) -> markup color style content |> printMarkedUp
    | MC (color, content)
    | MarkupC (color, content) -> markup color "" content |> printMarkedUp
    | MS (style, content)
    | MarkupS (style, content) -> markup "" style content |> printMarkedUp
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
        |> List.map (fun item -> 
            match item with
            | CO items
            | Collection items -> CO ([S bulletItemPrefix]@items)
            | BI items
            | BulletItems items -> failwith "Bullet items can't be used within bullet items, sry."
            | _ -> CO [S bulletItemPrefix; item])
        |> List.iter toConsole
    | Many values -> values |> List.iter (fun value -> printMarkedUpNL (standard value))
    | ManyMarkedUp markedUp -> markedUp |> List.iter toConsole