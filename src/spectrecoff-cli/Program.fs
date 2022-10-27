open Spectre.Console.Cli
open SpectreCoff.Cli.Commands

[<EntryPoint>]
let main argv =

    let app = CommandApp()
    app.Configure(fun config ->
        config.AddCommand<Output>("output")
            .WithDescription("Shows examples of printing things to the console.")
            |> ignore

        config.AddCommand<TableExample>("table")
            .WithDescription("Shows examples of tables.")
            |> ignore

        config.AddBranch("rule", fun(add: IConfigurator<RuleSettings>) ->
            add.AddCommand<RuleExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples for rendering rules.")
                |> ignore

            add.AddCommand<RuleDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the rule module.")
                |> ignore)

        config.AddBranch("figlet", fun(add: IConfigurator<FigletSettings>) ->
            add.AddCommand<FigletExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples for rendering figlets.")
                |> ignore

            add.AddCommand<FigletDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the figlet module.")
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

        config.AddBranch("panel", fun(add: IConfigurator<PanelSettings>) ->
            add.AddCommand<PanelExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples of panels.")
                |> ignore

            add.AddCommand<PanelDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the panel module.")
                |> ignore)

        config.AddBranch("bar", fun(add: IConfigurator<BarChartSettings>) ->
            add.AddCommand<BarChartExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples of bar charts.")
                |> ignore

            add.AddCommand<BarChartDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the bar charts module.")
                |> ignore)
        )

    app.Run(argv)