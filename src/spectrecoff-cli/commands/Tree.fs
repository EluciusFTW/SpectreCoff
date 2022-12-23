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

        let subNode value = 
            createNode (panel "" (P value)) []

        let nodes = 
            [ for i in 1 .. 50 -> (i, createNode (C $"{i}") []) ] 
            |> List.map (fun (index, node) -> 
                match index with
                | i when i % 15 = 0 -> attach [subNode "FizzBuzz"] node
                | i when i % 5 = 0 -> attach [subNode "Buzz"] node
                | i when i % 3 = 0 -> attach [subNode "Fizz"] node
                | _ -> node)
        
        tree (P "FizzBuzz-Tree!") nodes 
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
