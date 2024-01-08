# Canvas Module

This module provides functionality from the [canvas widget](https://spectreconsole.net/widgets/canvas) of _Spectre.Console_.

A canvas can be created using the canvas function,
```fs
canvas: Width -> Height -> Canvas
```
where `Width` and `Height` are two single-case discriminated unions wrapping integers.

The content of the canvas is created using the function:
```fs
withPixels: Pixels -> Color -> Canvas -> Canvas
```
The `Pixels` type is again a discriminated unions with several cases to efficiently provide pixels:
```fs
type Point = int*int
type Pixels = 
    | Pixel of Point
    | Pixels of Point list
    | Row of RowIndex
    | RowSegment of RowIndex*StartIndex*EndIndex
    | Column of ColumnIndex
    | ColumnSegment of ColumnIndex*StartIndex*EndIndex
    | Rectangle of Point*Point
```

### Example
We call this artistic masterpiece a _cryey_.
```fs
canvas (Width 12) (Height 12)
    |> withPixels (Rectangle (Point(0,0), Point(11,11))) Color.Yellow
    |> withPixels (Rectangle (Point(2,2), Point(4,3))) Color.Purple
    |> withPixels (Rectangle (Point(7,2), Point(9,3))) Color.Purple
    |> withPixels (ColumnSegment (ColumnIndex 8, StartIndex 4, EndIndex 7)) Color.Blue
    |> withPixels (RowSegment (RowIndex 9, StartIndex 3, EndIndex 8)) Color.Purple
    |> withPixels (Pixels [(3,10); (8,10)]) Color.Purple
    |> toOutputPayload
    |> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Canvas.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run canvas example
```