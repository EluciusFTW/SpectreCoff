# Tree Module
This module provides functionality from the [tree widget](https://spectreconsole.net/widgets/tree) of _Spectre.Console_.

A tree can be created by using one of the following functions,
```fs
tree: OutputPayload -> TreeNode list -> Tree
customTree: TreeLayout -> OutputPayload -> TreeNode list -> Tree
```
where the argument of type `OutputPayload` provides the content of the root node and the provided nodes will be connected to the root. The `tree` function uses
```fs
let mutable defaultTreeLayout: TreeLayout =
    { Sizing = Collapse
      Guides = SingleLine
      Look = calmLook }
```
as a default, while `customTree` accepts a layout instance.

Nodes are created in a similar fashion:
```fs
node: OutputPayload -> TreeNode list -> TreeNode
```

One can also attach more nodes to the root or another node later on, using one of these functions,
```fs
attach: TreeNode list -> TreeNode -> TreeNode
attachToRootFn: TreeNode list -> Tree -> Tree
```
where the node resp. tree to which nodes where attached is returned so the functions can be piped.

Finally, the tree can be mapped to an `OutputPayload` using `toOutputPayload` and be sent to the console via the `toConsole` function.

### Example
This is an implementation of the famous 'Fizz-Buzz Tree'.
```fs
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
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Tree.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run tree example
```