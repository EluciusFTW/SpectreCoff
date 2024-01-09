# Panel Module
This module provides functionality from the [panel widget](https://spectreconsole.net/widgets/panel) of _Spectre.Console_.

Panels can be created using one of these functions,
```fs
panel: (header: string) -> OutputPayload -> OutputPayload
customPanel: PanelLayout -> string -> OutputPayload -> OutputPayload
```
where the header can he a _marked up string_ and the content is an `OutputPayload`.

The `panel` function uses as a default
```fs
let mutable defaultPanelLayout: PanelLayout =
    { Border = BoxBorder.Rounded
      BorderColor = edgyLook.Color
      Sizing = Collapse
      Padding = AllEqual 2 }
```
while any other instance of that type can be passed in the `customPanel` function.

Finally, the panel is sent to the consolevia the `toConsole` function.

### Example
```fs
let payload = ... // some OutputPayload
let header = P "Sample panel header" |> toMarkedUpString

payload
|> panel header
|> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Panel.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run panel example
```