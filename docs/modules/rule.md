# Rule Module
This module provides functionality from the [rule widget](https://spectreconsole.net/widgets/rule) of _Spectre.Console_.

Rules can be created using one of these functions:
```fs
rule: string -> OutputPayload
alignedRule: Alignment -> string -> OutputPayload
```

The former will use the default alignment stored in this variable:
```fs
module SpectreCoff.Rule
    let mutable defaultAlignment = Center
```
Finally, the rule can be printed to the console using the `toConsole` function.

### Example
```fs
"Hello World"
|> alignedRule Left
|> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Rule.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run rule example
```