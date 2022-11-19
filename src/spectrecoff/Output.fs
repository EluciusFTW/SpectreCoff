module SpectreCoff.Output

open SpectreCoff.Layout
open Spectre.Console

// Styles
let mutable calmStyle = None
let mutable calmColor = Color.Blue

let mutable pumpedStyle = Italic
let mutable pumpedColor = Color.LightPink1

let mutable edgyStyle = Bold
let mutable edgyColor = Color.DarkRed

let mutable linkColor = pumpedColor

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

let toMarkup = 
    Markup 

let pumped content = markupString (Some pumpedColor) (Some pumpedStyle) content
let edgy content = markupString (Some edgyColor) (Some edgyStyle) content
let calm content = markupString (Some calmColor) calmStyle content

let printMarkedUpInline content = AnsiConsole.Markup $"{content}"
let printMarkedUp content = AnsiConsole.Markup $"{content}{System.Environment.NewLine}"

type OutputPayload =
    | MarkupCS of Color * Layout.Style * string
    | MarkupC of Color * string
    | MarkupS of Layout.Style * string
    | Link of string
    | LinkWithLabel of string*string
    | Emoji of string
    | Calm of string
    | Pumped of string
    | Edgy of string
    | Vanilla of string
    | Collection of OutputPayload list
    | BulletItems of OutputPayload list
    | NewLine
    | Many of string list
    | ManyMarkedUp of OutputPayload list
    | Renderable of Rendering.IRenderable

// Short aliases
let MCS = MarkupCS
let MC = MarkupC
let MS = MarkupS
let C = Calm
let P = Pumped
let E = Edgy
let V = Vanilla
let CO = Collection
let BI = BulletItems
let NL = NewLine

let toMarkedUpString (payload: OutputPayload) =
    match payload with
    | Calm value -> value |> calm
    | Pumped value -> value |> pumped 
    | Edgy value -> value |> edgy
    | Vanilla value -> value 
    | MarkupCS (color, style, content) -> content |> markupString (Some color) (Some style)
    | MarkupC (color, content) -> content |> markupString (Some color) None
    | MarkupS (style, content) -> content |> markupString None (Some style)
    | Link link -> link |> markupWithColorAndStyle linkColor "link"
    | LinkWithLabel (label, link) -> label |> markupWithColorAndStyle linkColor $"link={link}"
    | Emoji emoji -> emoji |> padEmoji 
    | _ -> ""

// NOTE: Still unhandled cases.
let payloadToRenderable (payload: OutputPayload) =
    match payload with  
    | Renderable r -> r
    | _ -> payload    
        |> toMarkedUpString
        |> toMarkup 
        :> Rendering.IRenderable

let toConsoleInline (payload: OutputPayload) =
    match payload with
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
    | Calm value -> 
        value 
        |> calm 
        |> printMarkedUpInline
    | Pumped value ->
        value 
        |> pumped 
        |> printMarkedUpInline
    | Edgy value -> 
        value
        |> edgy 
        |> printMarkedUpInline
    | Vanilla value -> value |> printMarkedUpInline
    | _ -> ()

let rec toConsole (payload: OutputPayload) =
    match payload with
    | MarkupCS (color, style, content) -> 
        content 
        |> markupString (Some color) (Some style)
        |> printMarkedUp
    | MarkupC (color, content) -> 
        content 
        |> markupString (Some color) None
        |> printMarkedUp
    | MarkupS (style, content) -> 
        content
        |> markupString None (Some style)
        |> printMarkedUp
    | Calm value -> 
        value 
        |> calm 
        |> printMarkedUp
    | Pumped value -> 
        value 
        |> pumped 
        |> printMarkedUp
    | Edgy value -> 
        value 
        |> edgy 
        |> printMarkedUp
    | Vanilla value -> value |> printMarkedUp
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
    | Renderable renderable -> renderable |> AnsiConsole.Write
    | Collection items ->
        items |> List.iter toConsoleInline
        printfn ""
    | BulletItems items ->
        items
        |> List.map (fun item ->
            match item with
            | Collection items -> CO ([C bulletItemPrefix]@items)
            | BulletItems _ -> failwith "Bullet items can't be used within bullet items, sry."
            | _ -> CO [C bulletItemPrefix; item])
        |> List.iter toConsole
    | Many values -> values |> List.iter (fun value -> calm value |> printMarkedUp)
    | ManyMarkedUp markedUp -> markedUp |> List.iter toConsole