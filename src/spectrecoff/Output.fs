module SpectreCoff.Output

open SpectreCoff.Layout
open Spectre.Console

// Styles
let mutable standardStyle = None
let mutable standardColor = Color.Blue

let mutable emphasizeStyle = Italic
let mutable emphasizeColor = Color.LightPink1

let mutable warningStyle = Bold
let mutable warningColor = Color.DarkRed

let mutable linkColor = emphasizeColor

// Special strings
let mutable bulletItemPrefix = " + "

// Basic output
let private markupWithColor color content = 
    $"[{color}]{Markup.Escape content}[/]"

let private markupWithStyle style content = 
    $"[{style}]{Markup.Escape content}[/]"

let private markupWithColorAndStyle color (style: string) content = 
    $"[{color} {style}]{Markup.Escape content}[/]"

let private padEmoji (emoji: string) = 
    match emoji.StartsWith ":" with
    | true -> emoji
    | false -> $":{emoji}:"

let markupString (color: Color option) (style: Layout.Style option) content =
    match style with
    | None -> 
        match color with
        | None -> content
        | Some c -> markupWithColor c content
    | Some s ->
        match color with
        | None -> markupWithStyle s content
        | Some c -> markupWithColorAndStyle c (stringifyStyle s) content

let toMarkup content =
    Markup content

let emphasize content = markupString (Some emphasizeColor) (Some emphasizeStyle) content
let warn content = markupString (Some warningColor) (Some warningStyle) content
let standard content = markupString (Some standardColor) standardStyle content

let printMarkedUpInline content = AnsiConsole.Markup $"{content}"
let printMarkedUp content = AnsiConsole.Markup $"{content}{System.Environment.NewLine}"

type OutputPayload =
    | MCS of Color * Layout.Style * string
    | MarkupCS of Color * Layout.Style * string
    | MC of Color * string
    | MarkupC of Color * string
    | MS of Layout.Style * string
    | MarkupS of Layout.Style * string
    | S of string
    | Standard of string
    | E of string
    | Emphasize of string
    | W of string
    | Warn of string
    | C of string
    | Custom of string
    | Link of string
    | LinkWithLabel of string*string
    | Emoji of string
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
    | Link link -> 
        link 
        |> markupWithColorAndStyle linkColor "link"
        |> printMarkedUpInline 
    | LinkWithLabel (label, link) -> 
        label 
        |> markupWithColorAndStyle linkColor $"link={link}"
        |> printMarkedUp 
    | Emoji emoji ->
        emoji 
        |> padEmoji 
        |> printMarkedUpInline
    | _ -> ()

let rec toConsole (payload: OutputPayload) =
    match payload with
    | MCS (color, style, content)
    | MarkupCS (color, style, content) -> 
        content 
        |> markupString (Some color) (Some style)
        |> printMarkedUp
    | MC (color, content)
    | MarkupC (color, content) -> 
        content 
        |> markupString (Some color) None
        |> printMarkedUp
    | MS (style, content)
    | MarkupS (style, content) -> 
        content
        |> markupString None (Some style)
        |> printMarkedUp
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
    | Link link -> 
        link 
        |> markupWithColorAndStyle linkColor "link"
        |> printMarkedUp 
    | LinkWithLabel (label, link) -> 
        label 
        |> markupWithColorAndStyle linkColor $"link={link}"
        |> printMarkedUp 
    | Emoji emoji -> 
        emoji 
        |> padEmoji 
        |> printMarkedUp 
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
            | BI _
            | BulletItems _ -> failwith "Bullet items can't be used within bullet items, sry."
            | _ -> CO [S bulletItemPrefix; item])
        |> List.iter toConsole
    | Many values -> values |> List.iter (fun value -> printMarkedUp (standard value))
    | ManyMarkedUp markedUp -> markedUp |> List.iter toConsole