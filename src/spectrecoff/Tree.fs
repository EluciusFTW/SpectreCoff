[<AutoOpen>]
module SpectreCoff.Tree

open Spectre.Console
open SpectreCoff.Layout
open SpectreCoff.Output

type GuideStyle =
    | Ascii
    | SingleLine
    | DoubleLine
    | BoldLine

type TreeLayout =
    { Sizing: SizingBehaviour
      Guides: GuideStyle
      Look: Look }

let defaultTreeLayout: TreeLayout =
    { Sizing = Collapse
      Guides = SingleLine
      Look = calmLook }

let private toNullable option =
    match option with
    | None -> System.Nullable<_> ()
    | Some a -> System.Nullable<_> a

let private applyLayout layout (root: Tree) =
    match layout.Sizing with
    | Expand -> root.Expanded <- true
    | Collapse -> root.Expanded <- false

    match layout.Guides with
    | Ascii -> root.Guide <- TreeGuide.Ascii
    | SingleLine -> root.Guide <- TreeGuide.Line
    | DoubleLine -> root.Guide <- TreeGuide.DoubleLine
    | BoldLine -> root.Guide <- TreeGuide.BoldLine

    root.Style <- toSpectreStyle layout.Look
    root

let private toRenderable tree = 
    tree
    :> Rendering.IRenderable
    |> Renderable

let attach (nodes: TreeNode list) (node: TreeNode) =
    nodes |> List.iter (fun n -> node.AddNode n |> ignore)
    node

let attachToRoot (nodes: TreeNode list) (root: Tree) =
    nodes |> List.iter (fun n -> root.AddNode n |> ignore)
    root

let node (content: OutputPayload) (nodes: TreeNode list) =
    content
    |> payloadToRenderable
    |> TreeNode
    |> attach nodes

let customTree (layout: TreeLayout) (rootContent: OutputPayload ) (nodes: TreeNode list) =
    rootContent
    |> payloadToRenderable
    |> Tree
    |> applyLayout layout
    |> attachToRoot nodes
    |> toRenderable

let tree =
    customTree defaultTreeLayout
