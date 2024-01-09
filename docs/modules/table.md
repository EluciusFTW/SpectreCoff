# Table Module
This module provides functionality from the [table widget](https://spectreconsole.net/widgets/table) of _Spectre.Console_.

The table can be created by the two functions,
```fs
table: ColumnDefinition list -> Row list -> Table
customTable: TableLayout -> ColumnDefinition list -> Row list -> Table
```

where the `table` uses the default layout,
```fs
let mutable defaultTableLayout: TableLayout =
    {  Border = TableBorder.Rounded
       Sizing = Expand
       Alignment = Left
       HideHeaders = false
       HideFooters = false }
```
while the `customTable` function accepts the layout as a further argument.

The table functions also takes in the column definitions which are of this form:
```fs
type ColumnDefinition =
    { Header: OutputPayload
      Footer: Option<OutputPayload>
      Layout: Option<ColumnLayout> }
```

If no layout is provided in the column definition, another default is used, namely:
```fs
let mutable defaultColumnLayout: ColumnLayout =
    { Alignment = Center
      LeftPadding = 2
      RightPadding = 2
      Wrap = true }
```

The colum definition can be creted manually, or with these pipable builder functions:
```fs
column: OutputPayload -> ColumDefinition
withFooter: OutputPayload -> ColumnDefinition -> ColumnDefinition
withLayout: ColumnLayout -> ColumnDefinition -> ColumnDefinition
withFooters: OutputPayload list -> ColumnDefinition list -> ColumnDefinition list
withSameLayout: ColumnLayout -> ColumnDefinition list -> ColumnDefinition list
```

The data of the table is provided by instances of the `Row` type, which is the same type as the one used in the [Grid module](Grid.md). Rows can also be added after creation, using:
```fs
addRowToTable: table -> row -> unit
```
           
A title and a caption can also be added to the table using,
```fs
withTitle: string -> Table -> Table
withCaption: string -> Table -> Table
```
The title is currently shown in the _pumped style_ and the caption in the 
_calm style_ (maybe we'll expose changing styles for these later as well).

Finally, the table can be mapped to an `OutputPayload` using `toOutputPayload` and be sent to the console via the `toConsole` function.

### Example
```fs
let columns = [
    column (Calm "Number")
    column (Calm "Square")
    column (Pumped "Cube") |> withLayout { defaultColumnLayout with Alignment = Right }
]
let rows = [ for i in 1 .. 5 -> Numbers [i; pown i 2; pown i 3] ]
let powerTable = table columns rows

// Print the table with five rows
powerTable
|> toOutputPayload
|> toConsole

// Add some more rows and 
[ for i in 6 .. 10 -> Numbers [i; pown i 2; pown i 3] ] 
|> List.iter (fun row -> addRowToTable powerTable row)  // or use: powerTable.addRow row

// Print it again
powerTable 
|> toOutputPayload
|> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Table.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run table example
```