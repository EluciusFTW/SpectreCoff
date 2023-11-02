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
                BL
                image |> toOutputPayload
                P "Take full advantage of "; LinkWithLabel ("ImageSharp", "https://github.com/SixLabors/ImageSharp"); P "to manipulate your images!"; BL
                P "For example, rotate the image by 45°:"
                BL
                rotatedImage.toOutputPayload
            ] |> toConsole
        }
        |> Async.AwaitTask
        |> Async.RunSynchronously
        0

open SpectreCoff.Cli.Documentation

type CanvasImageDocumentation() =
    inherit Command<CanvasImageSettings>()
    interface ICommandLimiter<CanvasImageSettings>

    override _.Execute(_context, _settings) =
        setDocumentationStyle
        Many [
            docSynopsis "CanvasImage module" "This submodule provides functionality from the canvas image widget of Spectre.Console" "widgets/canvas-image"
            BL
            C "The canvas image can be used using the canvasImage function:"
            BI [
                P "canvasImage: ImageSource -> CanvasImage"
            ]
            BL
            C "CanvasImage is an";P "IRenderable"; C "which can be converted into an"; P "OutputPayload";
            C "using the"; P "toOutputPayload"; C "function from the output module. The same is also available as an extension method."
            BL
            C "The"; P "ImageSource"; C "union type enables the use of different sources for the image:"
            BI [
                Many [P "Bytes"; C "of"; P "Byte[]"]
                Many [P "Stream"; C "of"; P "Stream"]
                Many [P "Path"; C "of"; P "string"]
            ]
            BL
            C "The canvas image module exposes the mutable variable"; P "maxWidth."
            C "Unsurprisingly, it sets the max width of the created images."
            BL
            C "Same as in spectre, the"; LinkWithLabel ("ImageSharp", "https://github.com/SixLabors/ImageSharp"); C "Api can be used to transform the created images."
        ] |> toConsole
        0
