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
let markupString color style content = 
    match style with
    | "" -> $"[{color}]{Markup.Escape content}[/]"
    | _ -> 
        match color with
        | "" -> $"[{style}]{Markup.Escape content}[/]"
        | _ ->  $"[{color} {style}]{Markup.Escape content}[/]"

let toMarkup content = 
    Markup content

let emphasize content = markupString emphasizeColor emphasizeStyle content
let warn content = markupString warningColor warningStyle content
let standard content = markupString standardColor standardStyle content

let printMarkedUpInline content = AnsiConsole.Markup $"{content}"
let printMarkedUp content = AnsiConsole.Markup $"{content}{System.Environment.NewLine}"

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
    | NL
    | NewLine    
    | Many of string list
    | ManyMarkedUp of OutputPayload list

let toRenderablePayload (payload: OutputPayload) = 
    match payload with
    | S value
    | Standard value -> standard value |> toMarkup
    | E value
    | Emphasize value -> emphasize value|> toMarkup
    | W value
    | Warn value -> warn value |> toMarkup
    | C value
    | Custom value -> value |> toMarkup
    | NewLine -> "" |> toMarkup
    | _ -> "" |> toMarkup

let toConsoleInline (payload: OutputPayload) = 
    match payload with
    | S value
    | Standard value ->  printMarkedUpInline (standard value)
    | E value 
    | Emphasize value -> printMarkedUpInline (emphasize value)
    | W value
    | Warn value -> printMarkedUpInline (warn value)
    | C value
    | Custom value -> printMarkedUpInline value
    | _ -> ()

let rec toConsole (payload: OutputPayload) = 
    match payload with
    | MCS (color, style, content)
    | MarkupCS (color, style, content) -> markupString color style content |> printMarkedUp
    | MC (color, content)
    | MarkupC (color, content) -> markupString color "" content |> printMarkedUp
    | MS (style, content)
    | MarkupS (style, content) -> markupString "" style content |> printMarkedUp
    | S value
    | Standard value ->  printMarkedUp (standard value)
    | E value
    | Emphasize value -> printMarkedUp (emphasize value)
    | W value
    | Warn value -> printMarkedUp (warn value)
    | C value
    | Custom value -> printMarkedUp value
    | NL
    | NewLine -> printfn ""
    | CO items
    | Collection items -> 
        items |> List.iter toConsoleInline
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
    | Many values -> values |> List.iter (fun value -> printMarkedUp (standard value))
    | ManyMarkedUp markedUp -> markedUp |> List.iter toConsole