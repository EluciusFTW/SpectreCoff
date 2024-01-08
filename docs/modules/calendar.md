# Calendar Module
This module provides functionality from the [calendar widget](https://spectreconsole.net/widgets/calendar) of Spectre.Console.


The calendar can be created by providing the year and month to display:
```fs
calendar: Year -> Month -> Calendar
customCalendar: CalendarSettings -> Year -> Month -> Calendar
```
where `Year` and `Month` are single-case DUs wrapping an integer.

The calendar function above uses the `defaultCalendarSettings` instance:
```fs
let defaultCalendarSettings: CalendarSettings =
    {  Culture =  None
       HideHeaders = false
       HeaderLook = { calmLook with Decorations = [ Decoration.Bold ] }
       HighlightLook = { pumpedLook with Decorations = [ Decoration.Invert ] } }
```
which can be used as a starting point for custom settings.

An `Event` (a single-case discriminated union taking a `Year`, `Month` and `Day`) can be added to the calendar using
```fs
addEvent: Event -> Calendar -> Calendar
```

Finally, the calendar can be mapped to an `OutputPayload` using `toOutputPaylod` and be sent to the console via the `toConsole` function.

### Example
```fs
// Create a calendar starting with a month in focus
let calendar = calendar (Year 2021) (Month 11)

// Add events and print it to the console
calendar
|> addEvent (Event (Year 2021, Month 11, Day 09))
|> addEvent (Event (Year 2021, Month 11, Day 16))
|> toOutputPayload
|> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Calendar.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run calendar example
```
