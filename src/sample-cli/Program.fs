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
            
        config.AddCommand<PromptExample>("prompt")
            .WithAlias("t")
            .WithDescription("Shows examples of prompts.")
            |> ignore

        config.AddCommand<TableExample>("table")
            .WithAlias("t")
            .WithDescription("Shows examples of tables.")
            |> ignore)

    app.Run(argv)