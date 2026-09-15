# Figlet Module
This module provides functionality from the [figlet widget](https://spectreconsole.net/widgets/figlet) of _Spectre.Console_.

The figlet can be created with one of the following functions:
```fs
figlet: string -> OutputPayload
customFiglet: Alignment -> Color -> string -> OutputPayload
customFigletWithMode: FigletLayoutMode -> Alignment -> Color -> string -> OutputPayload
```

Three mutable variables determine the default layout and styling of a figlet,
```fs
module SpectreCoff.Figlet
    let mutable defaultAlignment = Center
    let mutable defaultColor = pumpedLook.Color
    let mutable defaultLayoutMode = FigletLayoutMode.FullSize
```
while the `customFiglet` function accepts an alignment and a color and does not use those defaults. It still takes the layout mode from `defaultLayoutMode` — use `customFigletWithMode` to set that per call as well.

The layout mode decides how tightly the letters are packed: `FullSize` gives every letter its full width, `Fitted` moves them together until they touch, and `Smushed` lets neighbouring letters overlap where their shapes allow it.

Finally, the figlet is sent to the console via the `toConsole` function.

### Example
```fs
"Star ..."
|> figlet
|> toConsole

"Wars!"
|> customFiglet Left Color.SeaGreen1
|> toConsole

"... and beyond"
|> customFigletWithMode FigletLayoutMode.Smushed Left Color.SeaGreen1
|> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Figlet.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):

```
dotnet run figlet example
```
