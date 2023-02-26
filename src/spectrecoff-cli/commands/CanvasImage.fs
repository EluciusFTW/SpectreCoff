namespace SpectreCoff.Cli.Commands

open System.Net.Http
open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff
open SixLabors.ImageSharp.Processing

type CanvasImageSettings() =
    inherit CommandSettings()

type CanvasImageExample() =
    inherit Command<CanvasImageSettings>()
    interface ICommandLimiter<CanvasImageSettings>

    override _.Execute(_context, _settings) =
        task {
        use client = new HttpClient()
        let! response = client.GetByteArrayAsync("https://sample-videos.com/img/Sample-png-image-500kb.png")
        let image = canvasImage (Bytes response)
        let rotatedImage = (canvasImage (Bytes response)).Mutate(fun ctx -> ctx.Rotate(45f) |> ignore)
        Many [
           P "Print an image directly to the console!"
           image |> toOutputPayload
           CO [P "Take full advantage of the"; LinkWithLabel ("ImageSharp", "https://github.com/SixLabors/ImageSharp"); P "to manipulate your images!"]
           P "For example, rotate the image by 45°:"
           rotatedImage |> toOutputPayload
           ] |> toConsole
        }
        |> Async.AwaitTask
        |> Async.RunSynchronously
        0

type CanvasImageDocumentation() =
    inherit Command<CanvasImageSettings>()
    interface ICommandLimiter<CanvasImageSettings>

    override _.Execute(_context, _settings) =
        Cli.Theme.setDocumentationStyle
        NewLine |> toConsole
        pumped "CanvasImage module"
        |> alignedRule Left
        |> toConsole

        Many [
            CO [
                C "This submodule provides functionality from the canvas image widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/canvas-image"
                C ")"
            ]
            NL
            C "The canvas image can be used using the canvasImage function:"
            BI [
                P "canvasImage: ImageSource -> CanvasImage"
            ]
            NL
            CO [C "The"; P "ImageSource"; C "union type enables the use of different sources for the image:"]
            BI [
                CO [P "Bytes"; C "of"; P "Byte[]"]
                CO [P "Stream"; C "of"; P "Stream"]
                CO [P "Path"; C "of"; P "String"]
            ]
            NL
            CO [C "The canvas image module exposes the mutable variable"; P "maxWidth."]
            C "Unsurprisingly, it sets the max width of the created images."
            NL
            CO [C "Same as in spectre, the"; LinkWithLabel ("ImageSharp", "https://github.com/SixLabors/ImageSharp"); C "Api can be used to transform the created images."]
        ] |> toConsole
        0
