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

        let nodes =
            [ for i in 1 .. 16 -> (i, node (Calm $"{i}") []) ]
            |> List.map (fun (index, currentNode) ->
                match index with
                | i when i % 15 = 0 -> attach [node (Edgy "FizzBuzz") []] currentNode
                | i when i % 5 = 0 -> attach [node (Pumped "Buzz") []] currentNode
                | i when i % 3 = 0 -> attach [node (Calm "Fizz") []] currentNode
                | _ -> currentNode)

        tree (Pumped "Fizz-Buzz Tree") nodes
        |> toOutputPayload
        |> toConsole
        0
