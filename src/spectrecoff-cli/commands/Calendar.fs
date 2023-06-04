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

type CalendarDocumentation() =
    inherit Command<CalendarSettings>()
    interface ICommandLimiter<CalendarSettings>

    override _.Execute(_context, _) =
        Cli.Theme.setDocumentationStyle
        Edgy "Sorry, this documentation is not available yet." |> toConsole
        0