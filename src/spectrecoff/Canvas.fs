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


type PixelPosition = int*int
type Pixel = PixelPosition*Color
type Pixels = 
    | Single of PixelPosition
    | Many of PixelPosition list
    | Row of int
    | RowSegment of int*int*int
    | Column of int
    | ColumnSegment of int*int*int
    | Rectangle of int*int*int*int

let canvas (width: Width) (height: Height) =
    Canvas (Width.unwrap width, Height.unwrap height) 
    
let private setPixel (canvas: Canvas) (pixel: Pixel) = 
    canvas.SetPixel(fst (fst pixel), snd (fst pixel), snd pixel) |> ignore
    ()

let rec getPixelPositions (canvas: Canvas) pixels = 
    match pixels with
    | Single p -> [p]
    | Many ps -> ps
    | RowSegment (row, s, e) -> [ for x in s .. e -> (x, row)] 
    | ColumnSegment (col, s, e) -> [ for y in s .. e -> (col, y)]
    | Row row -> getPixelPositions canvas (RowSegment (row, 0, canvas.Width))
    | Column column -> getPixelPositions canvas (ColumnSegment (column, 0, canvas.Height))
    | Rectangle (x1,y1,x2,y2) -> 
        [ for col in x1 .. x2 -> ColumnSegment(col, y1, y2) ] 
        |> List.map (getPixelPositions canvas)
        |> List.collect (fun positions -> positions)

let rec withPixels pixels color (canvas: Canvas) = 
    pixels
    |> getPixelPositions canvas
    |> List.iter (fun position -> setPixel canvas (position, color))
    canvas

type Canvas with
    member self.toOutputPayload = toOutputPayload self
