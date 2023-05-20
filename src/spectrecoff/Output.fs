[<AutoOpen>]
module SpectreCoff.Output

open SpectreCoff.Layout
open Spectre.Console
open System

// Styles
let mutable calmLook: Look = 
    { Color = Color.SteelBlue
      Decorations = [ Decoration.None ] }
let mutable pumpedLook: Look = 
    { Color = Color.DeepSkyBlue3_1
      Decorations = [ Decoration.Italic ]}

let mutable edgyLook: Look = 
    { Color = Color.DarkTurquoise
      Decorations = [ Decoration.Bold ] }

let mutable linkLook = 
    { Color = pumpedLook.Color
      Decorations = [ Decoration.Underline; Decoration.Italic ] }

let mutable bulletItemPrefix = " + "

let private padEmoji (emoji: string) =
    match emoji.StartsWith ":" with
    | true -> emoji
    | false -> $":{emoji}:"

let rec private joinSeparatedBy (separator: string) (strings: string list) =
    match strings with
    | [] -> ""
    | [s] -> s
    | head::tail -> head + separator + joinSeparatedBy separator tail

let private stringifyDecorations (decorations: Decoration list) = 
    decorations |> List.map (fun decoration -> decoration.ToString())

let private stringify colorOption decorations = 
    let parts = 
        match colorOption with 
        | Some color -> [color.ToString()]@(stringifyDecorations decorations)
        | None -> (stringifyDecorations decorations)
    parts |> joinSeparatedBy " "

 // Basic output
let markupString (colorOption: Color option) (decorations: Decoration list) content =
    $"[{stringify colorOption decorations}]{Markup.Escape content}[/]"

let markupLink link label =
    let linkCss = stringify (Some linkLook.Color) linkLook.Decorations
    match label with
    | "" -> $"[{linkCss} link]{Markup.Escape link}[/]"
    | _ -> $"[{linkCss} link={link}]{Markup.Escape label}[/]"

let pumped content = content |> markupString (Some pumpedLook.Color) (pumpedLook.Decorations)
let edgy content = content |> markupString (Some edgyLook.Color) (edgyLook.Decorations)
let calm content = content |> markupString (Some calmLook.Color)  (calmLook.Decorations)

let printMarkedUpInline content = AnsiConsole.Markup $"{content}"
let printMarkedUp content = AnsiConsole.Markup $"{content}{Environment.NewLine}"

let rec private joinSeparatedByNewline =
    joinSeparatedBy Environment.NewLine

let appendNewline content =
    content + Environment.NewLine

type OutputPayload =
    | NewLine
    | MarkupCD of Color * Decoration list * string
    | MarkupC of Color * string
    | MarkupD of Decoration list * string
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
let MCD = MarkupCD
let MC = MarkupC
let MD = MarkupD
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
    | MarkupCD (color, decorations, content) -> content |> markupString (Some color) decorations
    | MarkupC (color, content) -> content |> markupString (Some color) []
    | MarkupD (decorations, content) -> content |> markupString None decorations
    | Link link -> link |> markupLink "" 
    | LinkWithLabel (label, link) -> label |> markupLink link //$"link={link}"
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
