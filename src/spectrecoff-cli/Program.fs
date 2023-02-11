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
                .WithDescription("Shows examples of trees.")
                |> ignore

            add.AddCommand<TreeDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the tree module.")
                |> ignore)

        config.AddBranch("calendar", fun(add: IConfigurator<CalendarSettings>) ->
            add.AddCommand<CalendarExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples of calendars.")
                |> ignore

            add.AddCommand<CalendarDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the calendar module.")
                |> ignore)

        config.AddBranch("padder", fun(add: IConfigurator<PadderSettings>) ->
            add.AddCommand<PadderExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples of padders.")
                |> ignore

            add.AddCommand<PadderDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the padder module.")
                |> ignore)

        config.AddBranch("grid", fun(add: IConfigurator<GridSettings>) ->
            add.AddCommand<GridExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples of grids.")
                |> ignore

            add.AddCommand<GridDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the grid module.")
                |> ignore)

        config.AddBranch("textpath", fun(add: IConfigurator<TextpathSettings>) ->
            add.AddCommand<TextpathExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples of text paths.")
                |> ignore

            add.AddCommand<TextpathDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the text path module.")
                |> ignore)

        config.AddBranch("json", fun(add: IConfigurator<JsonSettings>) ->
            add.AddCommand<TextpathExample>("example")
                .WithAlias("e")
                .WithDescription("Shows examples of json output.")
                |> ignore

            add.AddCommand<TextpathDocumentation>("doc")
                .WithAlias("d")
                .WithDescription("Shows the documentation for the json module.")
                |> ignore)

        config.AddCommand<Progress>("progress")
            .WithDescription("Shows which modules from Spectre.Console have been ported to SpectreCoff.")
            |> ignore
        )

    app.Run(argv)
