open Spectre.Console.Cli
open SpectreCoff.Cli.Commands

[<EntryPoint>]
let main argv =

    let app = CommandApp()
    app.Configure(fun config ->
        config.AddBranch("output", fun(add: IConfigurator<OutputSettings>) ->
            add.AddCommand<OutputExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples of printing things to the console.")
                |> ignore

            add.AddCommand<OutputDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation of the output module.")
                |> ignore)

        config.AddBranch("table", fun(add: IConfigurator<TableSettings>) ->
            add.AddCommand<TableExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples of tables.")
                |> ignore

            add.AddCommand<TableDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation of the table module.")
                |> ignore)

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

        config.AddBranch("breakdown", fun(add: IConfigurator<BreakdownChartSettings>) ->
            add.AddCommand<BreakdownChartExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples of breakdown charts.")
                |> ignore

            add.AddCommand<BreakdownChartDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the breakdown chart module.")
                |> ignore)

        config.AddBranch("tree", fun(add: IConfigurator<TreeSettings>) ->
            add.AddCommand<TreeExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples of tree charts.")
                |> ignore

            add.AddCommand<TreeDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the tree module.")
                |> ignore)

        config.AddCommand<Progress>("progress")
            .WithDescription("Shows which modules from Spectre.Console have been ported to SpectreCoff.")
            |> ignore
        )

    app.Run(argv)