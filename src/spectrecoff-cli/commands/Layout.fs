namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff
open Spectre.Console

type LayoutSettings()  =
    inherit CommandSettings()

type LayoutExample() =
    inherit Command<LayoutSettings>()
    interface ICommandLimiter<LayoutSettings>

    override _.Execute(_context, _) =
        Many [
            C "Layouts can be nested and contain any kind of (or no) content. Here a little demonstration using canvas and panel:"
            BL
        ]
        |> toConsole

        let canvasContent =
            canvas (Width 12) (Height 12)
            |> withPixels (Rectangle (Point(0, 0), Point(11, 11))) Color.Yellow
            |> withPixels (Rectangle (Point(2, 2), Point(4, 3))) Color.Purple
            |> withPixels (Rectangle (Point(7, 2), Point(9, 3))) Color.Purple
            |> withPixels (ColumnSegment (ColumnIndex 8, StartIndex 4, EndIndex 7)) Color.Blue
            |> withPixels (RowSegment (RowIndex 9, StartIndex 3, EndIndex 8)) Color.Purple
            |> withPixels (Pixels [ (3, 10); (8, 10) ]) Color.Purple

        let panelContent =
            E "I am the upper panel"
            |> customPanel { defaultPanelLayout with Sizing = Expand } (P "Upper!" |> toMarkedUpString)
            |> payloadToRenderable

        let rootLayout =
            layout "Root"
            |> splitHorizontally
                [| layout "upper-child"
                   layout "lower-child" |> splitVertically [| layout "left-child" |> withRatio 3 ; layout "right-child" |> withRatio 2 |]
                |]
            |> setChildContent "upper-child" panelContent
            |> setChildContent "right-child" canvasContent

        rootLayout
        |> toOutputPayload
        |> toConsole
        0

open SpectreCoff.Cli.Documentation

type LayoutDocumentation() =
    inherit Command<LayoutSettings>()
    interface ICommandLimiter<LayoutSettings>

    override _.Execute(_context, _) =
        setDocumentationStyle
        Many [
            spectreDocSynopsis "Layout module" "This module provides functionality from the layout widget of Spectre.Console" "widgets/layout"
            C "A layout can be created by passing an identifier to the"; P "layout"; C"function:"
            funcsOutput [{ Name = "layout"; Signature = "string -> Layout" }]

            C "Layouts can be composed using the"; P "splitHorizontally"; C"and"; P "splitVertically"; C"functions:"
            funcsOutput [{ Name = "splitHorizontally"; Signature = "Layout array -> Layout -> Layout" }; { Name = "splitVertically"; Signature = "Layout array -> Layout -> Layout" }]
            C "Composed layouts can be further fine-tuned using several functions:"
            funcsOutput [{ Name = "withMinimumWidth"; Signature = "int -> Layout -> Layout" }; { Name = "withExplicitWidth"; Signature = "Nullable<int> -> Layout -> Layout"}; { Name = "withRatio"; Signature = "int -> Layout -> Layout" }]
            C "Corresponding functions are available to manipulate child layouts through their parents using their identifier:"
            funcsOutput [{ Name = "withChildMinimumWidth"; Signature = "string -> int -> Layout -> Layout" }; { Name = "withChildExplicitWidth"; Signature = "string -> Nullable<int> -> Layout -> Layout"}; { Name = "withChildRatio"; Signature = "string -> int -> Layout -> Layout" }]

            C "Layouts can be shown and hidden using the"; P "show"; C"and"; P "hide"; C"functions:"
            funcsOutput [{ Name = "show"; Signature = "Layout -> Layout" }; { Name = "hide"; Signature = "Layout -> Layout" }]
            C "Again, corresponding methods to target child layouts are available:"
            funcsOutput [{ Name = "showChild"; Signature = "string -> Layout -> Layout" }; { Name = "hideChild"; Signature = "string -> Layout -> Layout" }]

            C "A layout's content can be changed using the"; P "setContent"; C"and"; P "setChildContent"; C"functions:"
            funcsOutput [{ Name = "setContent"; Signature = "IRenderable -> Layout -> Layout" }; { Name = "setChildContent"; Signature = "string -> IRenderable -> Layout -> Layout" }]
        ] |> toConsole
        0
