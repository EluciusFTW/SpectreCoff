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
        |> withPixels (Rectangle (0,0,11,11)) Color.Yellow
        |> withPixels (Rectangle (2,2,4,3)) Color.Purple
        |> withPixels (Rectangle (7,2,9,3)) Color.Purple
        |> withPixels (ColumnSegment (ColumnIndex 8, StartIndex 4, EndIndex 7)) Color.Blue
        |> withPixels (RowSegment (RowIndex 9, StartIndex 3, EndIndex 8)) Color.Purple
        |> withPixels (MultiplePixels [(3,10); (8,10)]) Color.Purple
        |> toOutputPayload 
        |> toConsole

        0

type CanvasDocumentation() =
    inherit Command<CanvasSettings>()
    interface ICommandLimiter<CanvasSettings>

    override _.Execute(_context, _settings) =
        Cli.Theme.setDocumentationStyle
        EL |> toConsole
        
        pumped "Canvas module"
        |> alignedRule Left
        |> toConsole
        
        Many [
            Many [
                C "This module provides functionality from the canvas widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/canvas"
                C ")"
            ]
            EL
            Edgy "Sorry, this documentation is not available yet."
        ] |> toConsole
        0
