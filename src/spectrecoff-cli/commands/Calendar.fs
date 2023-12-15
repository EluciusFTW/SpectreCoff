namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff
open Spectre.Console

type CalendarSettings() =
    inherit CommandSettings()

type CalendarExample() =
    inherit Command<CalendarSettings>()
    interface ICommandLimiter<CalendarSettings>

    override _.Execute(_context, _) =

        // Create a calendar by providing a month and year
        let calendar = calendar (Year 2021) (Month 11)

        // Add an event and print it to the console
        calendar
        |> addEvent (Event (Year 2021, Month 11, Day 09))
        |> toOutputPayload
        |> toConsole

        // You can easily add more events to the same calendar and print it again
        calendar
        |> addEvent (Event (Year 2021, Month 11, Day 06))
        |> addEvent (Event (Year 2021, Month 11, Day 21))
        |> toOutputPayload
        |> toConsole

        // You can use custom settings to control how the calendar is displayed
        let settings =
          { defaultCalendarSettings with
              Culture = Some (Culture "de-DE")
              HeaderLook =
                { Color = edgyLook.Color
                  BackgroundColor = Some Color.Purple
                  Decorations = [ Decoration.Italic ] }
              HighlightLook =
                { Color = Some Color.Yellow
                  BackgroundColor = Some Color.Purple
                  Decorations = [ Decoration.Invert ] } }

        customCalendar settings (Year 2025) (Month 2)
        |> addEvent (Event (Year 2025, Month 2, Day 22))
        |> toOutputPayload
        |> toConsole
        0

open SpectreCoff.Cli.Documentation

type CalendarDocumentation() =
    inherit Command<CalendarSettings>()
    interface ICommandLimiter<CalendarSettings>

    override _.Execute(_context, _) =
        setDocumentationStyle
        Many [
          spectreDocSynopsis "Calendar module" "This module provides functionality from the calendar widget of Spectre.Console" "widgets/calendar"
          BL
          C "The calendar can be created by providing the year and month to display:"
          BI [
            Many [P "calendar: Year -> Month -> Calendar"; C "(here 'Year' and 'Month' are single-case DUs wrapping an integer)"]
            P "customCalendar: CalendarSettings -> Year -> Month -> Calendar"
          ]
          BL
          C "The calendar function above uses the"; P "(defaultCalendarSettings: CalendarSettings),"; C "which has the values:"
          BI [
              define "Culture" "the Culture used for displaying the dates (Culture Option, default: none)"
              define "HideHeaders" "determines whether headers are shown (bool, default: false)"
              define "HeaderLook" "defines the styling of the headers (Look)"
              define "HighlightLook" "defines the styling of the highlighted dates (Look)"
          ]
          BL
          C "Observe, that the functions return 'Calendar', not 'OutputPayload'."
          C "To print the calendar at a given time, one can map it to an 'OutputPayload' using the following function"
          C "(also available as an extension method on 'Calendar'):"
          BI [
            P "toOutputPayload: Table -> OutputPayload"
          ]

          BL
          C "An"; P "Event"; C "(a single-case DU taking a Year, Month and Day) can be added to the calendar using"
          BI [
            P "addEvent: Event -> Calendar -> Calendar"
          ]
          BL
          C "The calendar can be printed to the console with the"; P "toConsole"; C "function, after it is mapped to an 'OutputPayload'."
        ] |> toConsole
        0