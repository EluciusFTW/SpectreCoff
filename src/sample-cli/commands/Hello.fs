namespace SpectreFs.Commands

open Spectre.Console.Cli
open SpectreFs.Output

type HelloSettings() as self =
    inherit CommandSettings()

    [<CommandOption("-n|--name")>]
    member val name = "friend" with get, set

    override _.Validate() =
        match self.name.Length with
        | 1 -> Spectre.Console.ValidationResult.Error($"That's an awfully short name, I don't buy it.")
        | _ -> Spectre.Console.ValidationResult.Success()

type Hello() =
    inherit Command<HelloSettings>()
    interface ICommandLimiter<HelloSettings>

    override _.Execute(_context, settings) = 
        // printMarkedUp $"Hello {emphasize settings.name}!"

        // printMarkedUp $"""See how I {emphasize "emphasized"} your name?"""
        // printMarkedUp $"""In the same way, I can also {warn "warn you"} about something."""
        
        // printMarkedUp $"""You can change how I behave, though ..."""
        // SpectreFs.Output.emphasizeStyle <- "fuchsia"
        // SpectreFs.Output.warningStyle <- "yellow"
        // printMarkedUp $"""Maybe {emphasize "these colors"} suit you {warn "better"}?"""
        
        // put [Standard "Hello "; Emphasized "from[...] "; StandardLine " [[//]]put!"; WarningLine "-_-_ [] ??"]
        
        // printfn ""
        // putSeparatedBy " *** " ["some"; Emphasized "things"; "are"; [1;2;3]; Warning "magic!"]

        
        emphasizeStyle <- "fuchsia underline"
        warningStyle <- "red"
        standardStyle <- "green"

        printfn ""
        putLines [
            "So ..."
            "This is how you'd output several lines."
            ""
            "Some are more important." |> Emphasized
            "Some [/ ... /] contain markup, but show it just fine," |> Warning
            ""
            [
                "Multiple things can be " |> Standard
                "printed " |> Emphasized
                "in one " |> Standard
                "line, too." |> Warning
            ]
            ""
            "It's possible to print arbitrary objects as well, like:"
            seq<obj> { "... tuples: "; (1,2) }
            seq<obj> { "... collections: "; seq<obj> { "Jane "; "Jon "; Emphasized "Carlos" } }
        ]
        0

