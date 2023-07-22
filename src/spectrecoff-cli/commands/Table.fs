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
            EL
        ] |> toConsole

        [ for i in 6 .. 10 -> Numbers [i; pown i 2; pown i 3] ] |> List.iter exampleTable.addRow
        Many [
            P "Rows can be added to the same table later on."
            exampleTable.toOutputPayload
            EL
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

type TableDocumentation() =
    inherit Command<TableSettings>()
    interface ICommandLimiter<TableSettings>

    override _.Execute(_context, _) =

        Cli.Theme.setDocumentationStyle

        EL |> toConsole
        pumped "Table module"
        |> alignedRule Left
        |> toConsole

        Many [
            C "This module provides functionality from the table widget of Spectre.Console ("
            Link "https://spectreconsole.net/widgets/table"
            C ")"
            EL
            C "The table can be used by the two functions:"
            BI [
                P "table: ColumnDefinition list -> Row list -> Table"
                P "customTable: TableLayout -> ColumnDefinition list -> Row list -> Table"
            ]
            Many [C "where the table function uses a"; P"defaultTableLayout"; C"variable for the layout, which can be modified as well."]
            EL
            C "Observe, that the functions return 'Table', not 'OutputPayload', as you might expect. That is because tables are modifyable objects that are often buil up iteratively (e.g., rows of data are added after creation.), whereas 'OutputPayload' is an immutable payload that is to be sent to the console. There is a mapping function to turn it into an 'OutputPayload': "
            BI [ P "toOutputPayload: Table -> OutputPayload" ]
            C "This function is also available as an extension method on Table."
            EL
            C "The 'TableLayout' is a record with the following properties:"
            BI [
                Many [P "Border"; C ": sets the style of the border (Spectre.TableBorder, default: ),"]
                Many [P "Sizing"; C ": determines whether the table is expanded or collapsed. (SizingBehaviour, default: ),"]
                Many [P "Alignment"; C ": aligns the content of all the columns. (Alignment, default: ),"]
                Many [P "HideHeaders"; C ": determines whether headers are shown (boolean, default: false),"]
                Many [P "HideFooters"; C ": determines whether footers are shown (boolean, default: false)."]
            ]
            EL
            C "In the table function, the columns are defined by passing in a list of 'ColumnDefinitions', which are records with these properties:"
            BI [
                Many [P "Header"; C ": the content of the header (OutputPayload),"]
                Many [P "Footer"; C ": the optional content of the footer (Option<OutputPayload>),"]
                Many [P "Layout"; C ": the optional column layout instance (Option<ColumnLayout>)."]
            ]
            EL
            C "In order to specify a column, you can instanciate such a record manually, or use one of these functions:"
            BI [
                P "column: OutputPayload -> ColumDefinition"
                P "withFooter: OutputPayload -> ColumnDefinition -> ColumnDefinition"
                P "withLayout: ColumnLayout -> ColumnDefinition -> ColumnDefinition"
                P "withFooters: OutputPayload list -> ColumnDefinition list -> ColumnDefinition list"
                P "withSameLayout: ColumnLayout -> ColumnDefinition list -> ColumnDefinition list"
            ]
            C "The column function constructs a column without footer and with the 'defaultColumnLayout'. The singular with* functions add a footer or layout, respectively, to a column. 'withSameLayout' adds the same layout to multiple columns at once, and withFooters zips footers to columns."
            EL
            C "As you can see above, the column layout can also be specified by an instance of the record 'ColumnLayout' with these properties:"
            BI [
                Many [P "Alignment"; C": aligns the content of this column (Alignment, default: Center),"]
                Many [P "LeftPadding"; C ": determines the left padding of the column content (int, default: 2),"]
                Many [P "RightPadding"; C ": determines the right padding of the column content (int, default: 2),"]
                Many [P "Wrap"; C "(boolean), determines whether column content wraps."]
            ]
            Many [C "The default values are part of the"; P "defaultColumnLayout"; C"instance, which, as usual, can be modified directly as well."]
            EL
            Many [C "The data of the table is provided by the Row type, which is the same type as the one used in the"; P "Grid"; C "module. Please see that documentation for more details."]
            EL
            C "A title and a caption can also be added to the table using:"
            BI [
                P "withTitle: string -> Table -> Table"
                P "withCaption: string -> Table -> Table"
            ]
            C "The title is currently shown in the pumped style, and the caption in the calm style (maybe we'll expose changing styles for these later as well)."
        ] |> toConsole
        0
