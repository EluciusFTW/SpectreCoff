# Calendar Module
This module provides functionality from the [calendar widget](https://spectreconsole.net/widgets/calendar) of Spectre.Console.

### Example
```fs
// Create a calendar starting with a month in focus
let calendar = calendar (Year 2021) (Month 11)

// Add an event and print it to the console
calendar
|> addEvent (Event (Year 2021, Month 11, Day 09))
|> toOutputPayload
|> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Calendar.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run calendar example
```
