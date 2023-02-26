[<AutoOpen>]
module SpectreCoff.CanvasImage

open System
open System.IO
open Spectre.Console

let mutable maxWidth = 16

type ImageSource =
| Bytes of Byte[]
| Stream of Stream
| Path of String

let canvasImage source =
    let image =
        match source with
        | Bytes bytes -> CanvasImage bytes
        | Stream stream -> CanvasImage stream
        | Path path -> CanvasImage path
    image.MaxWidth <- maxWidth
    image
