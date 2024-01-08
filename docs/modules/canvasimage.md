# CanvasImage Module
This module provides functionality from the [canvas image widget](https://spectreconsole.net/widgets/canvas-image) of _Spectre.Console_.

The canvas image can be created using the canvasImage function,"
```fs
canvasImage: ImageSource -> CanvasImage
```
where the `ImageSource` union type enables the use of different sources for the image:
```fs
type ImageSource =
    | Bytes of Byte[]
    | Stream of Stream
    | Path of string
```

The canvas image module exposes the mutable variable `maxWidth` (with a default of 16), which controls the maximum width of the created images.

Same as in _Spectre.Console_, the [ImageSharp](https://github.com/SixLabors/ImageSharp) api can be used to transform the created images.

Finally, the canvasImage can be mapped to an `OutputPayload` using `toOutputPaylod` and be sent to the console via the `toConsole` function.

### Example
```fs
// print the original image
let image = canvasImage (Bytes imageAsBytes)
image |> toOutputPayload |> toConsole

// rotate it and print it again
image.Mutate(fun ctx -> ctx.Rotate(45f) |> ignore)
image |> toOutputPayload |> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/CanvasImage.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):

```
dotnet run canvasimage example
```