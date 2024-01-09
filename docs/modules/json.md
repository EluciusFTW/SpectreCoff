# Json Module
This module provides functionality from the [json widget](https://spectreconsole.net/widgets/json) of _Spectre.Console_.

Json can be printed in the console using the the json function:"
```fs
json: string -> OutputPayload
```

The styling of the json output is very customizable and uses nine (!) variables. Therefore we decided not to provide a `customJson` function in this module, but only allowing for styling via mutating the variables, which by default have these values:

```fs
module SpectreCoff.Json
    let mutable bracesLook = 
        { Color = calmLook.Color
          BackgroundColor = None
          Decorations = [ Decoration.None ] }

    let mutable bracketsLook = 
        { Color = calmLook.Color
          BackgroundColor = None
          Decorations = [ Decoration.None ] }

    let mutable colonLook = 
        { Color = calmLook.Color
          BackgroundColor = None
          Decorations = [ Decoration.None ] }

    let mutable commaLook = 
        { Color = calmLook.Color
          BackgroundColor = None
          Decorations = [ Decoration.None ] }

    let mutable memberLook = 
        { Color = pumpedLook.Color
          BackgroundColor = None
          Decorations = [ Decoration.Italic ] }

    let mutable stringLook = 
        { Color = edgyLook.Color
          BackgroundColor = None
          Decorations = [ Decoration.Bold ] }

    let mutable numberLook = 
        { Color = edgyLook.Color
          BackgroundColor = None
          Decorations = [ Decoration.Bold ] }

    let mutable booleanLook = 
        { Color = calmLook.Color
          BackgroundColor = None
          Decorations = [ Decoration.Bold ] }

    let mutable nullLook = 
        { Color = calmLook.Color
          BackgroundColor = None
          Decorations = [ Decoration.Dim ] }
```

Finally, the figlet is sent to the consolevia the `toConsole` function.

### Example
```fs
"""{ "hello": "world" }""" |> json |> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Json.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):

```
dotnet run json example
```