namespace SpectreCoff.Cli.Commands

open Spectre.Console;
open Spectre.Console.Cli
open SpectreCoff

type TableSettings()  =
    inherit CommandSettings()

type TableExample() =
    inherit Command<TableSettings>()
    interface ICommandLimiter<TableSettings>

    override _.Execute(_context, _) =

        let columns = [
            column (Calm "Number")
            column (Calm "Square")
            column (Pumped "Cube") |> withLayout { defaultColumnLayout with Alignment = Right }
        ]
        let rows = [ for i in 1 .. 5 -> Numbers [i; pown i 2; pown i 3] ]

        let exampleTable = table columns rows
        Many [
            P "This shows a table with a default and custom laid-out column."
            exampleTable.toOutputPayload
            BL
        ] |> toConsole

        [ for i in 6 .. 10 -> Numbers [i; pown i 2; pown i 3] ] |> List.iter exampleTable.addRow
        Many [
            P "Rows can be added to the same table later on."
            exampleTable.toOutputPayload
            BL
        ] |> toConsole

        // Now for a more complete example
        let columns = [
            column (Calm "Results")
            column (Pumped "Interpretations")
            |> withFooter (Pumped "Footer works, too!")
            |> withLayout { defaultColumnLayout with Alignment = Right }
        ]
        [ Payloads [ exampleTable.toOutputPayload;  MCD (Color.Red, [ Decoration.Bold ], "The bigger the exponent, the faster the sequence grows.") ]
          Payloads [ P "Under the table"; panel "Wow" (E "ye-haw") ]
          Strings [ "Sum"; "Last" ]
          Numbers [ 55; 10 ]
        ]
        |> customTable { defaultTableLayout with Sizing = Collapse; Border = TableBorder.DoubleEdge; HideHeaders = true } columns
        |> withTitle "Tables can be nested, contain other Payloads, have titles ..."
        |> withCaption "..., have footers, captions, be customized, ..."
        |> toOutputPayload
        |> toConsole
        0

open SpectreCoff.Cli.Documentation

type TableDocumentation() =
    inherit Command<TableSettings>()
    interface ICommandLimiter<TableSettings>

    override _.Execute(_context, _) =
        setDocumentationStyle
        Many [
            spectreDocSynopsis "Table module" "This module provides functionality from the table widget of Spectre.Console" "widgets/table"
            C "The table can be used by the two functions:"
            funcsOutput [
                { Name = "table"; Signature = "ColumnDefinition list -> Row list -> Table" }
                { Name = "customTable"; Signature = "TableLayout -> ColumnDefinition list -> Row list -> Table" }
            ]
            C "where the table function uses a"; P"defaultTableLayout"; C "variable for the layout, which can be modified as well."
            BL
            C "Observe, that the functions return 'Table', not 'OutputPayload', as you might expect. That is because tables are modifyable objects that are often buil up iteratively (e.g., rows of data are added after creation.), whereas 'OutputPayload' is an immutable payload that is to be sent to the console. There is a mapping function to turn it into an 'OutputPayload': "
            funcsOutput [{ Name = "toOutputPayload"; Signature = "Table -> OutputPayload" }]

            C "This function is also available as an extension method on Table."
            BL
            C "The 'TableLayout' is a record with the following properties:"
            valuesOutput [
                { Name = "Border"; Type = "Spectre.TableBorder"; DefaultValue = "Rounded"; Explanation = "sets the style of the border" }
                { Name = "Sizing"; Type = "SizingBehaviour"; DefaultValue = "Expand"; Explanation = "sets the style of the border" }
                { Name = "Alignment"; Type = "Alignment"; DefaultValue = "Left"; Explanation = "aligns the content of all the columns" }
                { Name = "HideHeaders"; Type = "Boolean"; DefaultValue = "false"; Explanation = "determines whether headers are shown" }
                { Name = "HideFooters"; Type = "Boolean"; DefaultValue = "false"; Explanation = "determines whether footers are shown" }
            ]
            BL
            C "In the table function, the columns are defined by passing in a list of 'ColumnDefinitions', which are records with these properties:"
            propsOutput [
                { Name = "Header"; Type = (nameof OutputPayload); Explanation = "the content of the header" }
                { Name = "Footer"; Type = (nameof Option<OutputPayload>); Explanation = "the optional content of the footer" }
                { Name = "Layout"; Type = (nameof Option<OutputPayload>); Explanation = "the optional column layout instance" }
            ]
            BL
            C "In order to specify a column, you can instanciate such a record manually, or use one of these functions:"
            funcsOutput [
                { Name = "column"; Signature = "OutputPayload -> ColumDefinition" }
                { Name = "withFooter"; Signature = "OutputPayload -> ColumnDefinition -> ColumnDefinition" }
                { Name = "withLayout"; Signature = "ColumnLayout -> ColumnDefinition -> ColumnDefinition" }
                { Name = "withFooters"; Signature = "OutputPayload list -> ColumnDefinition list -> ColumnDefinition list" }
                { Name = "withSameLayout"; Signature = "ColumnLayout -> ColumnDefinition list -> ColumnDefinition list" }
            ]
            BL
            C "The column function constructs a column without footer and with the 'defaultColumnLayout'. The singular with* functions add a footer or layout, respectively, to a column. 'withSameLayout' adds the same layout to multiple columns at once, and withFooters zips footers to columns."
            BL
            C "As you can see above, the column layout can also be specified by an instance of the record 'ColumnLayout' with these properties:"
            valuesOutput [
                { Name = "Alignment"; Type = "Alignment"; DefaultValue  = "Center"; Explanation = "aligns the content of this column" }
                { Name = "LeftPadding"; Type = "Integer"; DefaultValue  = "2"; Explanation = "determines the left padding of the column content" }
                { Name = "RightPadding"; Type = "Integer"; DefaultValue  = "2"; Explanation = "determines the right padding of the column content" }
                { Name = "Wrap"; Type = "Boolean"; DefaultValue  = ""; Explanation = "determines whether column content wraps." }
            ]
            C "The default values are part of the"; P "defaultColumnLayout"; C"instance, which, as usual, can be modified directly as well."
            BL
            C "The data of the table is provided by the Row type, which is the same type as the one used in the"; P "Grid"; C "module. Please see that documentation for more details."
            BL
            C "A title and a caption can also be added to the table using:"
            funcsOutput [
                { Name = "withTitle"; Signature = "string -> Table -> Table" }
                { Name = "withCaption"; Signature = "string -> Table -> Table" }
            ]
            C "The title is currently shown in the pumped style, and the caption in the calm style (maybe we'll expose changing styles for these later as well)."
        ] |> toConsole
        0
