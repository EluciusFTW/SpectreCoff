module Output
    open Spectre.Console

    let primaryStyle = "green"
    let warningStyle = "red"

    let markup style content = $"[{style}]{content}[/]"
    let emphasize content = markup primaryStyle content
    let warn content = markup warningStyle content

    let printMarkedUp content = AnsiConsole.Markup $"{content}{System.Environment.NewLine}"