# Grid Module
This module provides functionality from the [grid widget](https://spectreconsole.net/widgets/grid) of _Spectre.Console_.

The grid can be created using:,
```fs
grid: Row list -> Grid
```

A `Row` is a discriminated union type consisting in one of the following cases
```fs
type Row =
    | Payloads of OutputPayload list
    | Strings of string list
    | Numbers of int list
```

Rows can also be added after creation, using:
```fs
addRowToGrid: grid -> row -> unit
```

As a grid has no column definitions, it will have as many columns as the longest row. The other rows will be filled up with empty values.

Finally, the table can be mapped to an `OutputPayload` using `toOutputPayload` and be sent to the console via the `toConsole` function.

### Example
```fs
let example = grid [
    Numbers [1; 2]
    Strings ["One"; "Two"; "Three"]
]

// Print the example grid
example
|> toOutputPayload
|> toConsole

// A grid can be nested in another one
let payload = example |> toOutputPayload
let outerGrid = grid [ Payloads [ payload; Pumped "in between"; payload ] ] 

// Print the outer grid
outerGrid
|> toOutputPayload
|> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Grid.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run grid example
```