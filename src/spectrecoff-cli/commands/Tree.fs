namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type TreeSettings()  =
    inherit CommandSettings()

type TreeExample() =
    inherit Command<TreeSettings>()
    interface ICommandLimiter<TreeSettings>

    override _.Execute(_context, _) =


        let p v =  
            customPanel {defaultPanelLayout with Sizing = Collapse} $"{v}" (P $"This is a square number!")

        let subNode value = 
            createNode (p value) []

        let nodes = 
            [ for i in 1 .. 10 -> createNode (E $"{i}-th") [] ] 
            |> List.indexed
            |> List.map (fun (i,n) -> 
                match i%2 with 
                | 0 -> attach [subNode (i*i)] n
                | _ -> n)
        
        tree (P "Numbertree!") nodes 
        |> toOutputPayload 
        |> toConsole
        0

type TreeDocumentation() =
    inherit Command<TreeSettings>()
    interface ICommandLimiter<TreeSettings>

    override _.Execute(_context, _) =
        Cli.Theme.setDocumentationStyle
        Edgy "Sorry, this documentation is not available yet." |> toConsole    
        0
