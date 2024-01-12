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
