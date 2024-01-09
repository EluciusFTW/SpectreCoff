# Textpath Module
This module provides functionality from the [text path widget](https://spectreconsole.net/widgets/text-path) of _Spectre.Console_.

The text path can be used by the path function
```fs
path: string -> OutputPayload
alignedPath: Alignment -> string -> OutputPayload
```
`path` uses the default value for alignment stored in the `defaultAlignment` variable, while both functions use a few more values for layout and styling:
```fs
module SpectreCoff.Textpath
    let mutable defaultAlignment = Left
    let mutable stemLook = calmLook
    let mutable rootLook = calmLook
    let mutable separatorLook = pumpedLook
    let mutable leafLook = edgyLook
```

Finally, the path is sent to the console via the `toConsole` function.

### Example
```fs
"C:\\Temp\\local\\data.json"
|> path
|> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Textpath.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run textpath example
```