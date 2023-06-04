[<AutoOpen>]
module SpectreCoff.Output

open SpectreCoff.Layout
open Spectre.Console
open System

// Styles
let mutable calmLook: Look = 
    { Color = Some Color.SteelBlue
      BackgroundColor = None
      Decorations = [ Decoration.None ] }

let mutable pumpedLook: Look = 
    { Color = Some Color.DeepSkyBlue3_1
      BackgroundColor = None
      Decorations = [ Decoration.Italic ]}

let mutable edgyLook: Look = 
    { Color = Some Color.DarkTurquoise
      BackgroundColor = None
      Decorations = [ Decoration.Bold ] }

let mutable linkLook = 
    { Color = pumpedLook.Color
      BackgroundColor = None
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

let private stringify foregroundColorOption backgroundColorOption decorations = 
    let foregroundColorPart = 
        match foregroundColorOption with 
        | Some color -> [color.ToString()]
        | None -> []

    let backgroundColorPart = 
        match backgroundColorOption with 
        | Some color -> [$"on {color.ToString()}"]
        | None -> []
    
    let decorationParts = stringifyDecorations decorations
    
    foregroundColorPart
    @backgroundColorPart
    @decorationParts 
    |> joinSeparatedBy " "

let private stringifyLook look = 
    stringify look.Color look.BackgroundColor look.Decorations

 // Basic output
let markup style content = 
    $"[{style}]{Markup.Escape content}[/]"

let markupString (colorOption: Color option) (decorations: Decoration list) content =
    markup $"{stringify colorOption None decorations}" content

let markupLink link label =
    let style = stringifyLook linkLook
    match label with
    | "" -> markup $"{style} link" link
    | _ -> markup $"{style} link={link}" label

let pumped content = content |> markup (stringifyLook pumpedLook)
let edgy content = content |> markup (stringifyLook edgyLook)
let calm content = content |> markup (stringifyLook calmLook)

let printMarkedUpInline content = AnsiConsole.Markup $"{content}"
let printMarkedUp content = AnsiConsole.Markup $"{content}{Environment.NewLine}"

let rec private joinSeparatedByNewline =
    joinSeparatedBy Environment.NewLine

let appendNewline content =
    content + Environment.NewLine

type OutputPayload =
    | NewLine
    | MarkupL of Look * string
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
let ML = MarkupL
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
    | MarkupL (look, content) -> content |> markup (stringifyLook look)
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
