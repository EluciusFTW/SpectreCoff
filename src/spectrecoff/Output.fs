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

let toMarkup = Markup 

let pumped content = markupString (Some pumpedColor) (Some pumpedStyle) content
let edgy content = markupString (Some edgyColor) (Some edgyStyle) content
let calm content = markupString (Some calmColor) calmStyle content

let printMarkedUpInline content = AnsiConsole.Markup $"{content}"
let printMarkedUp content = Markup $"{content}{System.Environment.NewLine}" |> AnsiConsole.Write

type LinineOutputPayload =
    | MCS of Color * Layout.Style * string
    | MarkupCS of Color * Layout.Style * string
    | MC of Color * string
    | MarkupC of Color * string
    | MS of Layout.Style * string
    | MarkupS of Layout.Style * string
    | Link of string
    | LinkWithLabel of string*string
    | Emoji of string
    | C of string
    | Calm of string
    | P of string
    | Pumped of string
    | E of string
    | Edgy of string
    | V of string
    | Vanilla of string

type OutputPayload =
    | MCS of Color * Layout.Style * string
    | MarkupCS of Color * Layout.Style * string
    | MC of Color * string
    | MarkupC of Color * string
    | MS of Layout.Style * string
    | MarkupS of Layout.Style * string
    | Link of string
    | LinkWithLabel of string*string
    | Emoji of string
    | C of string
    | Calm of string
    | P of string
    | Pumped of string
    | E of string
    | Edgy of string
    | V of string
    | Vanilla of string
    | CO of OutputPayload list
    | Collection of OutputPayload list
    | BI of OutputPayload list
    | BulletItems of OutputPayload list
    | NL
    | NewLine
    | Many of string list
    | ManyMarkedUp of OutputPayload list
    | Ren of Rendering.IRenderable

let toMarkedUpString (payload: OutputPayload) =
    match payload with
    | C value
    | Calm value -> value |> calm
    | P value
    | Pumped value -> value |> pumped 
    | E value
    | Edgy value -> value |> edgy
    | V value
    | Vanilla value -> value 
    | MCS (color, style, content)
    | MarkupCS (color, style, content) -> content |> markupString (Some color) (Some style)
    | MC (color, content)
    | MarkupC (color, content) -> content |> markupString (Some color) None
    | MS (style, content)
    | MarkupS (style, content) -> content |> markupString None (Some style)
    | Link link -> link |> markupWithColorAndStyle linkColor "link"
    | LinkWithLabel (label, link) -> label |> markupWithColorAndStyle linkColor $"link={link}"
    | Emoji emoji -> emoji |> padEmoji 
    | _ -> ""

// NOTE: Still unhandled cases.
let payloadToRenderable (payload: OutputPayload) =
    match payload with  
    | Ren r -> r
    | CO
    | Collection -> 
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
    | C value
    | Calm value -> 
        value 
        |> calm 
        |> printMarkedUpInline
    | P value
    | Pumped value ->
        value 
        |> pumped 
        |> printMarkedUpInline
    | E value
    | Edgy value -> 
        value
        |> edgy 
        |> printMarkedUpInline
    | V value
    | Vanilla value -> value |> printMarkedUpInline
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
    | C value
    | Calm value -> 
        value 
        |> calm 
        |> printMarkedUp
    | P value
    | Pumped value -> 
        value 
        |> pumped 
        |> printMarkedUp
    | E value
    | Edgy value -> 
        value 
        |> edgy 
        |> printMarkedUp
    | V value
    | Vanilla value -> value |> printMarkedUp
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
    | Ren renderable -> renderable |> AnsiConsole.Write
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
            | Collection items -> CO ([C bulletItemPrefix]@items)
            | BI _
            | BulletItems _ -> failwith "Bullet items can't be used within bullet items, sry."
            | _ -> CO [C bulletItemPrefix; item])
        |> List.iter toConsole
    | Many values -> values |> List.iter (fun value -> calm value |> printMarkedUp)
    | ManyMarkedUp markedUp -> markedUp |> List.iter toConsole
