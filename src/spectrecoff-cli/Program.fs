open Spectre.Console.Cli
open SpectreCoff.Cli.Commands

[<EntryPoint>]
let main argv =

    let app = CommandApp()
    app.Configure(fun config ->
        config
            .AddBranch("output", fun(branchConfig: IConfigurator<OutputSettings>) ->
                branchConfig
                    .AddCommand<OutputExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of printing things to the console.")
                    |> ignore

                branchConfig
                    .AddCommand<OutputDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation of the output module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("table", fun(branchConfig: IConfigurator<TableSettings>) ->
                branchConfig
                    .AddCommand<TableExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of tables.")
                    |> ignore

                branchConfig
                    .AddCommand<TableDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation of the table module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("rule", fun(branchConfig: IConfigurator<RuleSettings>) ->
                branchConfig
                    .AddCommand<RuleExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples for rendering rules.")
                    |> ignore

                branchConfig
                    .AddCommand<RuleDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the rule module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("figlet", fun(branchConfig: IConfigurator<FigletSettings>) ->
                branchConfig
                    .AddCommand<FigletExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples for rendering figlets.")
                    |> ignore

                branchConfig
                    .AddCommand<FigletDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the figlet module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("prompt", fun(branchConfig: IConfigurator<PromptSettings>) ->
                branchConfig
                    .AddCommand<PromptExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of prompts.")
                    |> ignore

                branchConfig
                    .AddCommand<PromptDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the prompt module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("panel", fun(branchConfig: IConfigurator<PanelSettings>) ->
                branchConfig
                    .AddCommand<PanelExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of panels.")
                    |> ignore

                branchConfig
                    .AddCommand<PanelDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the panel module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("bar", fun(branchConfig: IConfigurator<BarChartSettings>) ->
                branchConfig
                    .AddCommand<BarChartExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of bar charts.")
                    |> ignore

                branchConfig
                    .AddCommand<BarChartDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the bar charts module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("breakdown", fun(branchConfig: IConfigurator<BreakdownChartSettings>) ->
                branchConfig
                    .AddCommand<BreakdownChartExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of breakdown charts.")
                    |> ignore

                branchConfig
                    .AddCommand<BreakdownChartDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the breakdown chart module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("tree", fun(branchConfig: IConfigurator<TreeSettings>) ->
                branchConfig
                    .AddCommand<TreeExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of trees.")
                    |> ignore

                branchConfig
                    .AddCommand<TreeDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the tree module.")
                    |> ignore
            )|> ignore

        config
            .AddBranch("calendar", fun(branchConfig: IConfigurator<CalendarSettings>) ->
                branchConfig
                    .AddCommand<CalendarExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of calendars.")
                    |> ignore

                branchConfig
                    .AddCommand<CalendarDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the calendar module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("padder", fun(branchConfig: IConfigurator<PadderSettings>) ->
                branchConfig
                    .AddCommand<PadderExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of padders.")
                    |> ignore

                branchConfig
                    .AddCommand<PadderDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the padder module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("grid", fun(branchConfig: IConfigurator<GridSettings>) ->
                branchConfig
                    .AddCommand<GridExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of grids.")
                    |> ignore

                branchConfig
                    .AddCommand<GridDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the grid module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("textpath", fun(branchConfig: IConfigurator<TextpathSettings>) ->
                branchConfig
                    .AddCommand<TextpathExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of text paths.")
                    |> ignore

                branchConfig
                    .AddCommand<TextpathDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the text path module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("json", fun(branchConfig: IConfigurator<JsonSettings>) ->
                branchConfig
                    .AddCommand<JsonExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of json output.")
                    |> ignore

                branchConfig
                    .AddCommand<JsonDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the json module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("canvas", fun(branchConfig: IConfigurator<CanvasSettings>) ->
                branchConfig
                    .AddCommand<CanvasExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of a drawn canvas.")
                    |> ignore

                branchConfig
                    .AddCommand<CanvasDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the canvas module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("canvasimage", fun(branchConfig: IConfigurator<CanvasImageSettings>) ->
                branchConfig
                    .AddCommand<CanvasImageExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of canvas image output.")
                    |> ignore

                branchConfig
                    .AddCommand<CanvasImageDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the canvas image module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("layout", fun(branchConfig: IConfigurator<LayoutSettings>) ->
                branchConfig
                    .AddCommand<LayoutExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of layout module.")
                    |> ignore

                branchConfig
                    .AddCommand<LayoutDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the layout module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("status", fun(branchConfig: IConfigurator<StatusSettings>) ->
                branchConfig
                    .AddCommand<StatusExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of the status module.")
                    |> ignore

                branchConfig
                    .AddCommand<StatusDocumentation>("doc")
                    .WithAlias("d")
                    .WithDescription("Shows the documentation for the status module.")
                    |> ignore
            ) |> ignore

        config
            .AddCommand<Progress>("progress")
            .WithDescription("Shows which modules from Spectre.Console have been ported to SpectreCoff.")
            |> ignore

        config.AddBranch("theme", fun(add: IConfigurator<ThemeSettings>) ->
            add.AddCommand<ThemeExample>("example")
                .WithDescription("Shows example output for the given theme")
                |> ignore

            add.AddCommand<ListThemes>("list")
                .WithDescription("Shows a list of built-in themes")
                |> ignore) |> ignore
        )

    app.Run(argv)
