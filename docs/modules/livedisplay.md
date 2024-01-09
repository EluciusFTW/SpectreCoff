# LiveDisplay Module
This module provides functionality from the [live display](https://spectreconsole.net/live/live-display) of _Spectre.Console_.

Live display can be started by passing an `IRenderable` to the `start` or the `startWithCustomConfig` functions:
```fs
start: IRenderable -> LiveDisplayOperation
startWithCustomConfig: LiveDisplayConfiguration -> IRenderable -> LiveDisplayOperation
```
Any changes to the `IRenderable` will be reflected in real time. The `LiveDisplayOperation` is just a type alias,

```fs
type LiveDisplayOperation = LiveDisplayContext -> Task<unit>
```

The `start` function uses the default configuration:
```fs
let defaultConfiguration: LiveDisplayConfiguration =
    { AutoClear = false
      Overflow = None
      Cropping = None }
```

### Example
This example simulates computing squares and cuebs of numbers on a really (!) slow computer:
```fs
// Define some columns for the table
let columns = [
    column (Calm "Number")
    column (Calm "Square")
    column (Pumped "Cube")
]

// Creates am empty (Spectre.Console.Table: IRenderable) with the given columns
let powerTable = table columns []

// Adds a row with powers of 'index' to the table
let addRow index table =
    Numbers [index; pown index 2; pown index 3] |> addRowToTable table

// This is the actual 'slow operation'. calculating the powers of 1 - 20
let operation (context: LiveDisplayContext) =
    task {
        for i in 1 .. 20 do
            exampleTable |> addRow i
            context.Refresh()
            do! Task.Delay(200)
    }

// This executes the operation and prints it
(start powerTable operation).Wait()
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/LiveDisplay.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run live-display example
```