# Figlet Module
This module provides functionality from the [figlet widget](https://spectreconsole.net/widgets/figlet) of _Spectre.Console_.

The figlet can be created with one of the following functions:
```fs
figlet: string -> OutputPayload
customFiglet: Alignment -> Color -> string -> OutputPayload
```

Two mutable variables determine the default layout and styling of a figlet,
```fs
module SpectreCoff.Figlet
    let mutable defaultAlignment = Center
    let mutable defaultColor = pumpedLook.Color
```
while the `customFiglet` function accepts corresponding values and does not use the defaults.

Finally, the figlet is sent to the console ia the `toConsole` function.

### Example
```fs
"Star ..."
|> figlet
|> toConsole

"Wars!"
|> customFiglet Left Color.SeaGreen1
|> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Figlet.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):

```
dotnet run figlet example
```