# Padder Module
This module provides functionality from the [padder widget](https://spectreconsole.net/widgets/padder) of _Spectre.Console_.

```fs
pad: (top: int) -> (right: int) -> (bottom: int) -> (left: int) -> OutputPayload -> OutputPayload
padTop: int -> OutputPayload -> OutputPayload
padRight: int -> OutputPayload -> OutputPayload
padBottom: int -> OutputPayload -> OutputPayload
padLeft: int -> OutputPayload -> OutputPayload
padHorizontal: int -> OutputPayload -> OutputPayload
padVertical: int -> OutputPayload -> OutputPayload
padSymmetric: (leftRight: int) -> (topBottom: int) -> OutputPayload -> OutputPayload
padAll: int -> OutputPayload -> OutputPayload
```
Note that all padding functions return the element and can hence be piped.

### Example
This prints the payload twice with the same padding, achieved in two different ways:
```fs
payload 
|> padHorizontal 2
|> toConsole

payload 
|> padRight 2 
|> padLeft 2
|> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Padder.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run padder example
```