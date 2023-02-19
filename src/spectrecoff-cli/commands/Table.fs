namespace SpectreCoff.Cli.Commands

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
            NewLine
        ] |> toConsole

        [ for i in 6 .. 10 -> Numbers [i; pown i 2; pown i 3] ] |> List.iter exampleTable.addRow
        Many [
            P "Rows can be added to the same table later on."
            exampleTable.toOutputPayload
            NewLine
        ] |> toConsole

        P "Tables can be nested, contain other Payloads, have footers, be customized, ..." |> toConsole
        let columns = [
            column (Calm "Results")
            column (Pumped "Interpretations") 
            |> withFooter (Pumped "Footer works, too!") 
            |> withLayout { defaultColumnLayout with Alignment = Right }
        ]
        
        [ Payloads [ exampleTable.toOutputPayload;  MCS (Spectre.Console.Color.Red, Bold, "The bigger the exponent, the faster the sequence grows.") ]
          Payloads [ P "Under the table"; panel "Wow" (E "ye-haw") ] 
          Strings [ "Sum"; "Last" ]
          Numbers [ 55; 10 ]
        ]
        |> customTable { defaultTableLayout with Sizing = Collapse; Border = Spectre.Console.TableBorder.DoubleEdge; HideHeaders = true } columns
        |> withTitle "Custom"
        |> withCaption "This is a custom table"
        |> toOutputPayload
        |> toConsole
        0

type TableDocumentation() =
    inherit Command<TableSettings>()
    interface ICommandLimiter<TableSettings>

    override _.Execute(_context, _) =
        
        Cli.Theme.setDocumentationStyle
        
        NewLine |> toConsole
        pumped "Table module"
        |> alignedRule Left
        |> toConsole

        Many [
            CO [
                C "This module provides functionality from the table widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/rule"
                C ")"
            ]
            NewLine
            C "The table can be used by the two functions:"
            BI [ 
                P "table: ColumnDefinition list -> Row list -> OutputPayload"
                P "customTable: TableLayout -> ColumnDefinition list -> Row list -> OutputPayload"
            ]
            CO [C "where the table function uses a"; P"defaultTableLayout"; C"variable for the layout, which can be modified as well."]
            NewLine
            C "The TableLayout is a record with the following properties:"
            BI [
                CO [P "Border"; C ": sets the style of the border (Spectre.TableBorder, default: ),"]
                CO [P "Sizing"; C ": determines whether the table is expanded or collapsed. (SizingBehaviour, default: ),"]
                CO [P "Alignment"; C ": aligns the content of all the columns. (Alignment, default: ),"]
                CO [P "HideHeaders"; C ": determines whether headers are shown (boolean, default: false),"]
                CO [P "HideFooters"; C ": determines whether footers are shown (boolean, default: false)."]
            ]
            NewLine
            C "In the table function, the columns are defined by passing in a list of ColumnDefinitions, which are records with these properties:"
            BI [
                CO [P "Header"; C ": the content of the header (OutputPayload),"]
                CO [P "Footer"; C ": the optional content of the footer (Option<OutputPayload>),"]
                CO [P "Layout"; C ": the optional column layout instance (Option<ColumnLayout>)."]
            ]
            NewLine
            C "In order to specify a column, you can instanciate such a record manually, or use one of these functions:"
            BI [
                P "column: OutputPayload -> ColumDefinition"
                P "withFooter: OutputPayload -> ColumnDefinition -> ColumnDefinition"
                P "withLayout: ColumnLayout -> ColumnDefinition -> ColumnDefinition"
                P "withFooters: OutputPayload list -> ColumnDefinition list -> ColumnDefinition list"
                P "withSameLayout: ColumnLayout -> ColumnDefinition list -> ColumnDefinition list"
            ]
            C "The column function constructs a column without footer and with the defaultColumnLayout. The singular with* functions add a footer or layout, respectively, to a column."
            C "withSameLayout adds the same layout to multiple columns at once, and withFooters zips footers to columns."
            NewLine
            C "As you can see above, the column layout can also be specified by an instance of the record ColumnLayout with these properties:"
            BI [
                CO [P "Alignment"; C": aligns the content of this column (Alignment, default: Center),"]
                CO [P "LeftPadding"; C ": determines the left padding of the column content (int, default: 2),"]
                CO [P "RightPadding"; C ": determines the right padding of the column content (int, default: 2),"]
                CO [P "Wrap"; C "(boolean), determines whether column content wraps."]
            ]
            CO [C "The default values are part of the"; P "defaultColumnLayout"; C"instance, which, as usual, can be modified directly as well."]
            NewLine
            CO [C "The data of the table is provided by the Row type, which is the same type as the one used in the"; P "Grid"; C "module. Please see that documentation for more details."]
        ] |> toConsole
        0
