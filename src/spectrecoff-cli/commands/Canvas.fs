namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type CanvasSettings() =
    inherit CommandSettings()

type CanvasExample() =
    inherit Command<CanvasSettings>()
    interface ICommandLimiter<CanvasSettings>

    override _.Execute(_context, _settings) =
        canvas (Width 12) (Height 12)
        |> withPixels (Rectangle (Point(0,0), Point(11,11))) Color.Yellow
        |> withPixels (Rectangle (Point(2,2), Point(4,3))) Color.Purple
        |> withPixels (Rectangle (Point(7,2), Point(9,3))) Color.Purple
        |> withPixels (ColumnSegment (ColumnIndex 8, StartIndex 4, EndIndex 7)) Color.Blue
        |> withPixels (RowSegment (RowIndex 9, StartIndex 3, EndIndex 8)) Color.Purple
        |> withPixels (Pixels [(3,10); (8,10)]) Color.Purple
        |> toOutputPayload
        |> toConsole
        0

open SpectreCoff.Cli.Documentation

type CanvasDocumentation() =
    inherit Command<CanvasSettings>()
    interface ICommandLimiter<CanvasSettings>

    override _.Execute(_context, _settings) =
        setDocumentationStyle
        Many [
            spectreDocSynopsis "Canvas module" "This module provides functionality from the canvas widget of Spectre.Console" "widgets/canvas"
            C "A canvas can be created using the canvas function:"
            funcsOutput [{ Name = "canvas"; Signature = "Width -> Height -> Canvas" }]
            C "where"; P "Width"; C "and"; P "Height"; C "are two single-case DUs wrapping integers."
            BL
            C "The content of the canvas is created using the function:"
            funcsOutput [{ Name = "withPixels"; Signature = "Pixels -> Color -> Canvas -> Canvas" }]
            BL
            C "The"; P "Pixel"; C "type is again a DU with several cases to efficiently provide pixels:"
            discriminatedUnionOutput [
                { Label = "Pixel"; Args = ["Point"]; Explanation = "Sets a single pixel at the given position (Point is a DU taking two integers)" } 
                { Label = "Pixels"; Args = ["Point list"]; Explanation = "Sets all pixels provided in the list" } 
                { Label = "Row"; Args = ["RowIndex"]; Explanation = "Draws a row across the whole canvas (RowIndex wraps an integer)" } 
                { Label = "RowSegment"; Args = ["RowIndex"; "StartIndex"; "EndIndex"]; Explanation = "Draws a segment of a row (Start- and EmdIndex wrap an integer as well)" }
                { Label = "Column"; Args = ["ColumnIndex"]; Explanation = "Draws a column across the whole canvas (ColumnIndex wraps an integer)" }
                { Label = "ColumnSegment"; Args = ["ColumnIndex"; "StartIndex"; "EndIndex"]; Explanation = "Draws a segment of a column" }
                { Label = "Rectangle"; Args = ["Pixelposition"; "Pixelposition"]; Explanation = "Draws a rectangle spanned by the two points" }
            ]
            BL
            C "The canvas can be mapped to an output payload using"; P "toOutputPayload"; C "and then printed to the console with the"; P "toConsole"; C "function:"
        ] |> toConsole
        0
