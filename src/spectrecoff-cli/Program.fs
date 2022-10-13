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

        config.AddCommand<TableExample>("table")
            .WithAlias("t")
            .WithDescription("Shows examples of tables.")
            |> ignore

        config.AddBranch("rule", fun(add: IConfigurator<RuleSettings>) ->
            add.AddCommand<RuleExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples for rendering rules.")
                |> ignore

            add.AddCommand<RuleDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the prompt module.")
                |> ignore)

        config.AddBranch("prompt", fun(add: IConfigurator<PromptSettings>) ->
            add.AddCommand<PromptExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples of prompts.")
                |> ignore

            add.AddCommand<PromptDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the prompt module.")
                |> ignore)
        )

    app.Run(argv)