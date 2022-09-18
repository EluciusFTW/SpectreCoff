open Spectre.Console.Cli
open SpectreFs.Commands

[<EntryPoint>]
let main argv =

    let app = CommandApp()
    app.Configure(fun config ->
        config.AddCommand<Hello>("greet")
            .WithAlias("g")
            .WithDescription("Greets the user running the application.")
            |> ignore

        config.AddCommand<TableExample>("table")
            .WithAlias("t")
            .WithDescription("Shows usage of tables.")
            |> ignore)

    app.Run(argv)