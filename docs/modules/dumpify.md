# Dumpify Module
This module provides functionality from the [Dumpify](https://github.com/MoaidHathot/Dumpify) Nuget package. 

_Dumping_ is a side-effect of printing an instance of any type and returning it, so it can be injected in any call chain:
```fs
dump: 'a -> 'a
customDump: DumpifyOptions -> 'a -> 'a
```
`dump` will use the `Dumpify.defaultOptions` variable,
```fs
let defaultOption: DumpifyOptions =
    { Members = None
      Color = None
      Output = None
      Table = None
      TypeNames = None
      UseDescriptors = false }
```
while the `customDump` function accepts options. As always, the default options can be modified globally as well.

For more details on each option, please consult the documentation of _Dumpify_.

### Example
```fs
someInstance |> dump |> ignore
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Dumpify.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):

```
dotnet run dumpify example
```