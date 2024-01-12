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
        maxWidth <- 50
        task {
            use client = new HttpClient()
            let! response = client.GetByteArrayAsync("https://upload.wikimedia.org/wikipedia/commons/5/57/Fsharp_logo.png")
            let image = canvasImage (Bytes response)
            let rotatedImage = (canvasImage (Bytes response)).Mutate(fun ctx -> ctx.Rotate(45f) |> ignore)
            Many [
                P "Print an image directly to the console!"
                BL
                image |> toOutputPayload
                P "Take full advantage of "; LinkWithLabel ("ImageSharp", "https://github.com/SixLabors/ImageSharp"); P "to manipulate your images!"
                P "For example, rotate the image by 45°:"
                BL
                rotatedImage.toOutputPayload
            ] |> toConsole
        }
        |> Async.AwaitTask
        |> Async.RunSynchronously
        0
