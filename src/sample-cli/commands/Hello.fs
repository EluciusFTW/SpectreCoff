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

        let sequenceOfObjects = seq<obj> { Emphasized "Jane "; Warning "Jon "; Standard "Carlos" } 
        let nestedSequenceOfObjects = seq<obj> { "... collections: "; seq<obj> { "Jane "; "Jon "; Emphasized "Carlos" } }
        let listOfMarkedUp = [ Emphasized "Jane "; Warning "Jon "; Standard "Carlos" ]

        // 1st Version
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
                "in one line, too." |> Warning
            ]
            ""
            "It's possible to print arbitrary objects as well, like:"
            sequenceOfObjects
            nestedSequenceOfObjects
        ]

        
        // 2nd Version
        let friends = [ E "Jane "; W "Jon "; S "Carlos" ]
        printfn ""
        
        putComplexLines [
            S "So ..."
            S "This is how you'd output several lines."
            NL 
            E "Some are more important."
            W "Some [/ ... /] contain markup, but show it just fine," 
            CO [
                S "Multiple things can be "; 
                E "printed"; 
                W " in one line, too." 
            ]
            CO (sequenceOfObjects |> Seq.map OB |> List.ofSeq )
            CO friends
            NL
            S "Let me be clearer. Have you seen: "
            BI friends
        ]

        bulletItemPrefix <- "  ->> "
        putComplexLines [
            S "HAVE YOU SEEN: "
            BI friends
        ]

        putStandardLines [
            "Sometimes you"
            "Just want to"
            "Write a decent text"
            "Without being bothered by"
            "Symbols, types and other things ... "
        ]
        // printfn ""
        // putComplexThings [
        //     S "Hello!"; NL
        //     S "Hi there, "; E "dude!"; NL 
        //     S "Have you seen"; SP; CO friends; S "?"
        // ]

        0

