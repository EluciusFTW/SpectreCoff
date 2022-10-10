open Spectre.Console.Cli
open SpectreFs.Sample.Commands

[<EntryPoint>]
let main argv =

    let app = CommandApp()
    app.Configure(fun config ->
        config.AddCommand<Output>("output")
            .WithAlias("o")
            .WithDescription("Shows examples of printing things to the console.")
            |> ignore

        config.AddCommand<RuleExample>("rule")
            .WithAlias("r")
            .WithDescription("Shows examples for rendering rules.")
            |> ignore

        config.AddCommand<TableExample>("table")
            .WithAlias("t")
            .WithDescription("Shows examples of tables.")
            |> ignore

        config.AddBranch("prompt", fun(add: IConfigurator<PromptSettings>) ->
            add.AddCommand<PromptExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples of prompts.")
                |> ignore

            add.AddCommand<PromptDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Creates a .ffrc containing the working directory as well as all found variables.")
                |> ignore)
        )


    app.Run(argv)