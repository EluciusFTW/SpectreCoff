[<AutoOpen>]
module SpectreCoff.CanvasImage

open System
open System.IO
open Spectre.Console

let mutable maxWidth = 16

type ImageSource =
    | Bytes of Byte[]
    | Stream of Stream
    | Path of string

let canvasImage source =
    let image =
        match source with
        | Bytes bytes -> CanvasImage bytes
        | Stream stream -> CanvasImage stream
        | Path path -> CanvasImage path
    image.MaxWidth <- maxWidth
    image

type CanvasImage with
    member self.toOutputPayload = toOutputPayload self
