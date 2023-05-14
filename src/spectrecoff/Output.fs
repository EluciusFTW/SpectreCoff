[<AutoOpen>]
module SpectreCoff.Output

open SpectreCoff.Layout
open Spectre.Console
open System

// Styles
let mutable calmLook: Look = 
    { Color = Color.SteelBlue
      Decoration = Decoration.None }
let mutable pumpedLook: Look = 
    { Color = Color.DeepSkyBlue3_1
      Decoration = Decoration.Italic }

let mutable edgyLook: Look = 
    { Color = Color.DarkTurquoise
      Decoration = Decoration.Bold }

let mutable linkColor = pumpedLook.Color

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

let markupString (color: Color option) (style: Decoration option) content =
    match style with
    | None ->
        match color with
        | None -> content
        | Some c -> markupWithColor c content
    | Some s ->
        match color with
        | None -> markupWithStyle s content
        | Some c -> markupWithColorAndStyle c (s.ToString()) content

let pumped content = content |> markupString (Some pumpedLook.Color) (Some pumpedLook.Decoration)
let edgy content = content |> markupString (Some edgyLook.Color) (Some edgyLook.Decoration)
let calm content = content |> markupString (Some calmLook.Color)  (Some calmLook.Decoration)

let printMarkedUpInline content = AnsiConsole.Markup $"{content}"
let printMarkedUp content = AnsiConsole.Markup $"{content}{Environment.NewLine}"

let rec private joinSeparatedBy (separator: string) (strings: string list) =
    match strings with
    | [] -> ""
    | [s] -> s
    | head::tail -> head + separator + joinSeparatedBy separator tail

let rec private joinSeparatedByNewline =
    joinSeparatedBy Environment.NewLine

let appendNewline content =
    content + Environment.NewLine

type OutputPayload =
    | NewLine
    | MarkupCS of Color * Decoration * string
    | MarkupC of Color * string
    | MarkupS of Decoration * string
    | Link of string
    | LinkWithLabel of string*string
    | Emoji of string
    | Calm of string
    | Pumped of string
    | Edgy of string
    | Vanilla of string
    | Collection of OutputPayload list
    | BulletItems of OutputPayload list
    | Many of OutputPayload list
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

let toOutputPayload value =
    value
    :> Rendering.IRenderable
    |> Renderable

let rec toMarkedUpString (payload: OutputPayload) =
    match payload with
    | Calm content -> content |> calm
    | Pumped content -> content |> pumped
    | Edgy content -> content |> edgy
    | Vanilla content -> content
    | MarkupCS (color, decoration, content) -> content |> markupString (Some color) (Some decoration)
    | MarkupC (color, content) -> content |> markupString (Some color) None
    | MarkupS (decoration, content) -> content |> markupString None (Some decoration)
    | Link link -> link |> markupWithColorAndStyle linkColor "link"
    | LinkWithLabel (label, link) -> label |> markupWithColorAndStyle linkColor $"link={link}"
    | Emoji emoji -> emoji |> padEmoji
    | NewLine -> Environment.NewLine
    | Collection items ->
        items
        |> List.map (fun item ->
            match item with
            | Renderable _ -> failwith "Renderables can't be used in collections."
            | BulletItems _ -> failwith "Bullet items can't be used within bullet items."
            | Collection items ->
                items
                |> List.map toMarkedUpString
                |> joinSeparatedBy " "
            | _ -> toMarkedUpString item)
        |> joinSeparatedBy " "
    | BulletItems items ->
        items
        |> List.map (fun item ->
            match item with
            | Renderable _ -> failwith "Renderables can't be used within bullet items."
            | BulletItems _ -> failwith "Bullet items can't be used within bullet items."
            | Collection items -> CO ([C bulletItemPrefix]@items)
            | _ -> CO [C bulletItemPrefix; item])
        |> List.map toMarkedUpString
        |> joinSeparatedByNewline
    | Many payloads ->
        payloads
        |> List.map toMarkedUpString
        |> joinSeparatedByNewline
    | Renderable _ -> failwith "The payload type 'Renderable' is not stringifyable."

let rec payloadToRenderable (payload: OutputPayload) =
    match payload with
    | Renderable renderable -> renderable
    | Many payloads ->
        payloads
        |> List.map payloadToRenderable
        |> Rows
        :> Rendering.IRenderable
    | _ ->
        payload
        |> toMarkedUpString
        |> Markup
        :> Rendering.IRenderable

let toConsole (payload: OutputPayload) =
    payload
    |> payloadToRenderable
    |> AnsiConsole.Write
    AnsiConsole.WriteLine ""

type OutputPayload with
    member self.toMarkedUpString = toMarkedUpString self
    member self.toRenderable = payloadToRenderable self
    member self.toConsole = toConsole self
