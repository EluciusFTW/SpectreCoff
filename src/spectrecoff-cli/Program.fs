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
            ) |> ignore

        config
            .AddBranch("table", fun(branchConfig: IConfigurator<TableSettings>) ->
                branchConfig
                    .AddCommand<TableExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of tables.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("rule", fun(branchConfig: IConfigurator<RuleSettings>) ->
                branchConfig
                    .AddCommand<RuleExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples for rendering rules.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("figlet", fun(branchConfig: IConfigurator<FigletSettings>) ->
                branchConfig
                    .AddCommand<FigletExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples for rendering figlets.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("prompt", fun(branchConfig: IConfigurator<PromptSettings>) ->
                branchConfig
                    .AddCommand<PromptExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of prompts.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("panel", fun(branchConfig: IConfigurator<PanelSettings>) ->
                branchConfig
                    .AddCommand<PanelExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of panels.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("bar", fun(branchConfig: IConfigurator<BarChartSettings>) ->
                branchConfig
                    .AddCommand<BarChartExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of bar charts.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("breakdown", fun(branchConfig: IConfigurator<BreakdownChartSettings>) ->
                branchConfig
                    .AddCommand<BreakdownChartExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of breakdown charts.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("tree", fun(branchConfig: IConfigurator<TreeSettings>) ->
                branchConfig
                    .AddCommand<TreeExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of trees.")
                    |> ignore
            )|> ignore

        config
            .AddBranch("calendar", fun(branchConfig: IConfigurator<CalendarSettings>) ->
                branchConfig
                    .AddCommand<CalendarExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of calendars.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("padder", fun(branchConfig: IConfigurator<PadderSettings>) ->
                branchConfig
                    .AddCommand<PadderExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of padders.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("grid", fun(branchConfig: IConfigurator<GridSettings>) ->
                branchConfig
                    .AddCommand<GridExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of grids.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("textpath", fun(branchConfig: IConfigurator<TextpathSettings>) ->
                branchConfig
                    .AddCommand<TextpathExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of text paths.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("json", fun(branchConfig: IConfigurator<JsonSettings>) ->
                branchConfig
                    .AddCommand<JsonExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of json output.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("canvas", fun(branchConfig: IConfigurator<CanvasSettings>) ->
                branchConfig
                    .AddCommand<CanvasExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of a drawn canvas.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("canvasimage", fun(branchConfig: IConfigurator<CanvasImageSettings>) ->
                branchConfig
                    .AddCommand<CanvasImageExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of canvas image output.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("layout", fun(branchConfig: IConfigurator<LayoutSettings>) ->
                branchConfig
                    .AddCommand<LayoutExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of layout module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("live-display", fun(branchConfig: IConfigurator<LiveDisplaySettings>) ->
                branchConfig
                    .AddCommand<LiveDisplayExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of the live display module.")
                    |> ignore
            ) |> ignore
        config
            .AddBranch("progress", fun(branchConfig: IConfigurator<ProgressSettings>) ->
                branchConfig
                    .AddCommand<ProgressExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of the progress module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("status", fun(branchConfig: IConfigurator<StatusSettings>) ->
                branchConfig
                    .AddCommand<StatusExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of the status module.")
                    |> ignore
            ) |> ignore

        config
            .AddBranch("dumpify", fun(branchConfig: IConfigurator<DumpifySettings>) ->
                branchConfig
                    .AddCommand<DumpifyExample>("example")
                    .WithAlias("e")
                    .WithDescription("Shows examples of the dumpify module.")
                    |> ignore
            ) |> ignore

        config.AddBranch("theme", fun(add: IConfigurator<ThemeSettings>) ->
            add.AddCommand<ThemeExample>("example")
                .WithDescription("Shows example output for the given theme")
                |> ignore

            add.AddCommand<ListThemes>("list")
                .WithDescription("Shows a list of built-in themes")
                |> ignore) |> ignore
        )

    app.Run(argv)
