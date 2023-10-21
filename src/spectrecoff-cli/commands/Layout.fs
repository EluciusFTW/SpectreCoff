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
            |> withPixels (Rectangle (0,0,11,11)) Color.Yellow
            |> withPixels (Rectangle (2,2,4,3)) Color.Purple
            |> withPixels (Rectangle (7,2,9,3)) Color.Purple
            |> withPixels (ColumnSegment (ColumnIndex 8, StartIndex 4, EndIndex 7)) Color.Blue
            |> withPixels (RowSegment (RowIndex 9, StartIndex 3, EndIndex 8)) Color.Purple
            |> withPixels (MultiplePixels [(3,10); (8,10)]) Color.Purple

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
            docSynopsis "Layout module" "This module provides functionality from the layout widget of Spectre.Console" "widgets/layout"
            docMissing
        ] |> toConsole
        0
