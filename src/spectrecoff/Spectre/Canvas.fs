[<AutoOpen>]
module SpectreCoff.Canvas

open Spectre.Console

type Width = Width of int
[<RequireQualifiedAccess>]
module Width =
    let unwrap (Width w) = w

type Height = Height of int

[<RequireQualifiedAccess>]
module Height =
    let unwrap (Height h) = h

type ColumnIndex = ColumnIndex of int
type RowIndex = RowIndex of int
type StartIndex = StartIndex of int
type EndIndex = EndIndex of int
type Point = int*int

type Pixels = 
    | Pixel of Point
    | Pixels of Point list
    | Row of RowIndex
    | RowSegment of RowIndex*StartIndex*EndIndex
    | Column of ColumnIndex
    | ColumnSegment of ColumnIndex*StartIndex*EndIndex
    | Rectangle of Point*Point

let private setPixel (canvas: Canvas) color pixel = 
    canvas.SetPixel(fst pixel, snd pixel, color) |> ignore

let rec private getPixelPositions (canvas: Canvas) pixels = 
    match pixels with
    | Pixel p -> [p]
    | Pixels ps -> ps
    | RowSegment (RowIndex r, StartIndex s, EndIndex e) -> [ for x in s .. e -> (x, r)] 
    | ColumnSegment (ColumnIndex c, StartIndex s, EndIndex e) -> [ for y in s .. e -> (c, y)]
    | Row row -> getPixelPositions canvas (RowSegment (row, StartIndex 0, EndIndex canvas.Width))
    | Column column -> getPixelPositions canvas (ColumnSegment (column, StartIndex 0, EndIndex canvas.Height))
    | Rectangle ((x1, y1), (x2, y2)) -> 
        [ for col in x1 .. x2 -> ColumnSegment(ColumnIndex col, StartIndex y1, EndIndex y2) ] 
        |> List.map (getPixelPositions canvas)
        |> List.collect id

let withPixels pixels color (canvas: Canvas) = 
    pixels
    |> getPixelPositions canvas
    |> List.iter (setPixel canvas color)
    canvas

let canvas width height =
    Canvas (Width.unwrap width, Height.unwrap height) 

type Canvas with
    member self.toOutputPayload = toOutputPayload self
