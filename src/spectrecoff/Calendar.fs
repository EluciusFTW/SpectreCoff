[<AutoOpen>]
module SpectreCoff.Calendar

open Spectre.Console
open SpectreCoff.Layout
open SpectreCoff.Output

type Culture = Culture of string
let unwrapCulure (Culture c) = c

type Year = Year of int
let unwrapYear (Year y) = y

type Month = Month of int
let unwrapMonth (Month m) = m

type Day = Day of int
let unwrapDay (Day d) = d

type Event = 
    | Event of Year*Month*Day
    | SpecificEvent of string*Year*Month*Day

type CalendarSettings = 
    {  Culture: Culture Option;
       HideHeaders: bool;
       HeaderColor: Color;
       HeaderStyle: Style;
       HighlightColor: Color Option }

let defaultCalendarSettings = 
    {  Culture =  None
       HideHeaders = true
       HeaderColor = calmColor
       HeaderStyle = Bold
       HighlightColor= Some pumpedColor }

let addEvent (calendar: Calendar) event =
    match event with
    | Event (y,m,d) -> calendar.AddCalendarEvent (unwrapYear y, unwrapMonth m, unwrapDay d)
    | SpecificEvent (desc, y, m, d) -> calendar.AddCalendarEvent (desc, unwrapYear y, unwrapMonth m, unwrapDay d)

let applysettings (settings: CalendarSettings) (calendar: Calendar) = 
    match settings.Culture with 
    | Some culture -> calendar.Culture <- unwrapCulure culture |> System.Globalization.CultureInfo
    | _ -> ()

    match settings.HighlightColor with 
    | Some color -> calendar.HightlightStyle <- Style (color)
    | _ -> ()
    
    Event (Year 2021, Month 11, Day 05) |> addEvent calendar

let customCalendar settings year month day = 
    Calendar (unwrapYear year, unwrapMonth month, unwrapDay day)
    |> applysettings settings
    :> Rendering.IRenderable
    |> Renderable

let calendar =
    customCalendar defaultCalendarSettings