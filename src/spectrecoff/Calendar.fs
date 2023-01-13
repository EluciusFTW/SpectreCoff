[<AutoOpen>]
module SpectreCoff.Calendar

open Spectre.Console
open SpectreCoff.Output

type Culture = Culture of string
let unwrapCulure (Culture c) = c

type Year = Year of int
[<RequireQualifiedAccess>]
module Year = 
    let unwrap (Year y) = y

type Month = Month of int
[<RequireQualifiedAccess>]
module Month = 
    let unwrap (Month m) = m

type Day = Day of int
[<RequireQualifiedAccess>]
module Day = 
    let unwrap (Day d) = d

type Event = Event of Year * Month * Day
[<RequireQualifiedAccess>]
module Event =
    let toTuple event = 
        match event with
        | Event (y,m,d) -> (Year.unwrap y, Month.unwrap m, Day.unwrap d)

type CalendarSettings = 
    {  Culture: Culture Option;
       HideHeaders: bool;
       HeaderColor: Color;
       HeaderStyle: Decoration;
       HighlightColor: Color Option
       HighlightStyle: Decoration Option }

let defaultCalendarSettings = 
    {  Culture =  None
       HideHeaders = false
       HeaderColor = calmColor
       HeaderStyle = Decoration.Bold
       HighlightColor = Some pumpedColor
       HighlightStyle = Some Decoration.Invert }

let toOutputPayload calendar =
    calendar 
    :> Rendering.IRenderable 
    |> Renderable

let addEvent event (calendar: Calendar) =
    event 
    |> Event.toTuple
    |> calendar.AddCalendarEvent

let applysettings (settings: CalendarSettings) (calendar: Calendar) = 
    match settings.Culture with 
    | Some culture -> calendar.Culture <- unwrapCulure culture |> System.Globalization.CultureInfo
    | _ -> ()

    calendar.ShowHeader <- not settings.HideHeaders
    calendar.HeaderStyle <- Style (settings.HeaderColor, System.Nullable(), settings.HeaderStyle)
    
    match (settings.HighlightColor, settings.HighlightStyle) with 
    | Some color, Some style -> calendar.HightlightStyle <- Style (color, System.Nullable(), style)
    | Some color, None -> calendar.HightlightStyle <- Style (color)
    | None, Some style -> calendar.HightlightStyle <- Style (System.Nullable(), System.Nullable(), style)
    | _ -> ()

    calendar

let customCalendar settings year month = 
    Calendar (Year.unwrap year, Month.unwrap month)
    |> applysettings settings

let calendar =
    customCalendar defaultCalendarSettings

type Calendar with 
    member self.toOutputPayload = toOutputPayload self 
