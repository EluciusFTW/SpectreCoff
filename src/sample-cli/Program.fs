open Spectre.Console.Cli
open SpectreFs.Commands

[<EntryPoint>]
let main argv =

    let app = CommandApp()
    app.Configure(fun config ->
        config.AddCommand<Hello>("greet")
            .WithAlias("g")
            .WithDescription("Greets the user running the application.")
            |> ignore)

    app.Run(argv)