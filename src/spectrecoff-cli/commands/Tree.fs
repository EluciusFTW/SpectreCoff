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
            node (panel "" (P value)) []

        let nodes = 
            [ for i in 1 .. 16 -> (i, node (C $"{i}") []) ] 
            |> List.map (fun (index, node) -> 
                match index with
                | i when i % 15 = 0 -> attach [subNode "FizzBuzz"] node
                | i when i % 5 = 0 -> attach [subNode "Buzz"] node
                | i when i % 3 = 0 -> attach [subNode "Fizz"] node
                | _ -> node)
        
        tree (P "FizzBuzz-Tree!") nodes 
        |> toOutputPayload 
        |> toConsole

        customTree 
            { defaultTreeLayout with Look = { Color = Some Color.Green; BackgroundColor = Some Color.Grey; Decorations = [Decoration.Bold] } }
            (P "Custom-Tree!") 
            [ for i in 1 .. 3 -> node (C $"Node {i}!") [] ] 
        |> toOutputPayload
        |> toConsole
        0

open SpectreCoff.Cli.Documentation

type TreeDocumentation() =
    inherit Command<TreeSettings>()
    interface ICommandLimiter<TreeSettings>

    override _.Execute(_context, _) =
        let treeFn = 
            { Name = "tree"
              Signature = "OutputPayload -> TreeNode list -> Tree" }

        let customTreeFn = 
            { Name = "customTree"
              Signature = "TreeLayout -> OutputPayload -> TreeNode list -> Tree" }

        let nodeFn = 
            { Name = "node"
              Signature = "OutputPayload -> TreeNode list -> TreeNode" }

        let attachFn = 
            { Name = "attach" 
              Signature = "TreeNode list -> TreeNode -> TreeNode" }

        let attachToRootFn = 
            { Name = "attachToRoot"
              Signature = "TreeNode list -> Tree -> Tree" }

        let sizingProp: ValueDef = 
            { Name = "Sizing"
              Type = "SizingBehaviour"
              DefaultValue = "Collapse"
              Explanation = "The sizing of the tree nodes" }

        let guidesProp = 
            { Name = "Guides"
              Type = "GuideStyle"
              DefaultValue = "SingleLine"
              Explanation = "The style of the lines" }

        let lookProp = 
            { Name = "Look"
              Type = "Look"
              DefaultValue = "calmLook"
              Explanation = "The look of the nodes" }

        setDocumentationStyle
        Many[
            docSynopsis "Tree module" "This module provides functionality from the tree widget of Spectre.Console" "widgets/tree"
            C "A tree can be created by:"
            funcsOutput [treeFn; customTreeFn]
            C "The first argument of type 'OutputPayload' is the content of the root node, and the 'TreeNode list' are the first level branches."
            BL 
            C "The 'TreeLayout' that can be provided consists of:"
            valuesOutput [sizingProp; guidesProp; lookProp]
            BL
            C "The 'TreeNode' instances are created similarily, using"
            funcsOutput [nodeFn]
            BL
            C "If you want to attach more nodes to the root or anotehr node later on, you can use one of these functions:"
            funcsOutput [attachFn; attachToRootFn]
        ] |> toConsole
        0
