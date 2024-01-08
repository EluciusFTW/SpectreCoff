# Chart Modules
_SpectreCoff_ supports both the bar chart widget and the breakdown chart widget of _Spectre.Console_.

Both charts share the same `ChartItem` union type consisting of two options:
```fs
type ChartItem =
    | ChartItem of string * float                   // Label, Quantity
    | ChartItemWithColor of string * float * Color  // Label, Quantity, Color
```

If no color is explicitly defined, the colors will cycle through a set of colors defined in the `Colors` variable of the module. 
This variable can be overwritten with a custom set if the default one is not to your taste. Note that changes will apply to both types of charts.

## BarChart Module
This module provides functionality from the [bar chart widget](https://spectreconsole.net/widgets/barchart) of Spectre.Console.

The bar chart can be used using the barChart function:
```fs
barChart: string -> ChartItem list -> OutputPayload
```

Two mutable variables determine the layout of the chart:
```fs
module BarChart =
    let mutable width = 60              // Determines the width of the whole chart
    let mutable alignment = Center      // Determines the alignment of the chart's label
```

Finally, the chart is sent to the console ia the `toConsole` function.

### Example
```fs
let items = [
    ChartItem ("Apple", 12)
    ChartItem ("Orange", 3)
    ChartItem ("Banana", 6)
    ChartItemWithColor ("Kiwi", 2, Color.Green)
]

items
    |> barChart "Fruits"
    |> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/BarChart.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run barchart example
```

## BreakdownChart Module
This module provides functionality from the [breakdown chart widget](https://spectreconsole.net/widgets/breakdownchart) of Spectre.Console.

The breakdown chart can be used using the breakdownChart function:
```fs
breakdownChart: string -> ChartItem list -> OutputPayload
```

A mutable variables determines the width of the chart:
```fs
module BreakdownChart =
    let mutable width = 60
```

Finally, the chart is sent to the console ia the `toConsole` function.

### Example
```fs
let items = [
    ChartItem ("Refinements", 2)
    ChartItem ("Dailies", 2)
    ChartItem ("Retrospectives", 1)
    ChartItem ("Random meetings", 7)
    ChartItem ("Fixing bugs", 3)
    ChartItemWithColor ("Developing features", 1, Color.Red)
]

Many [
    Edgy "My life as a developer ..."
    BlankLine
    breakdownChart items
] |> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/breakdownChart.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run barchart example
```
