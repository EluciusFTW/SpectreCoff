module SpectreCoff.Tests.FigletTests

open Expecto
open FsUnit.Xunit
open Spectre.Console
open SpectreCoff

let private asFigletText payload =
    match payload with
    | Renderable renderable -> renderable :?> FigletText
    | other -> failwithf "expected a renderable figlet, got %A" other

let private withDefaults layoutMode alignment continuation =
    let previousMode = defaultLayoutMode
    let previousAlignment = defaultAlignment
    defaultLayoutMode <- layoutMode
    defaultAlignment <- alignment

    try
        continuation ()
    finally
        defaultLayoutMode <- previousMode
        defaultAlignment <- previousAlignment

// These swap the module level defaults, so they must not run alongside each other.
let private figletTests name tests =
    testList name tests |> testSequencedGroup "figlet"

[<Tests>]
let layoutModeTests =
    figletTests "figlet layout mode" [
        test "is FullSize out of the box" { defaultLayoutMode |> should equal FigletLayoutMode.FullSize }

        test "figlet takes the mode from the default" {
            withDefaults FigletLayoutMode.Smushed defaultAlignment (fun () ->
                figlet "Quince"
                |> asFigletText
                |> fun f -> f.LayoutMode |> should equal FigletLayoutMode.Smushed)
        }

        test "customFiglet also takes the mode from the default" {
            withDefaults FigletLayoutMode.Fitted defaultAlignment (fun () ->
                "Quince"
                |> customFiglet Left Color.SeaGreen1
                |> asFigletText
                |> fun f -> f.LayoutMode |> should equal FigletLayoutMode.Fitted)
        }

        test "customFigletWithMode overrides the default" {
            withDefaults FigletLayoutMode.FullSize defaultAlignment (fun () ->
                "Quince"
                |> customFigletWithMode FigletLayoutMode.Smushed Left Color.SeaGreen1
                |> asFigletText
                |> fun f -> f.LayoutMode |> should equal FigletLayoutMode.Smushed)
        }
    ]

[<Tests>]
let alignmentAndColorTests =
    figletTests "figlet styling" [
        test "customFiglet applies the given alignment" {
            "Quince"
            |> customFiglet Right Color.SeaGreen1
            |> asFigletText
            |> fun f -> f.Justification |> should equal (System.Nullable Justify.Right)
        }

        test "customFiglet applies the given color" {
            "Quince"
            |> customFiglet Left Color.SeaGreen1
            |> asFigletText
            |> fun f -> f.Color |> should equal (System.Nullable Color.SeaGreen1)
        }

        test "figlet takes the alignment from the default" {
            withDefaults defaultLayoutMode Left (fun () ->
                figlet "Quince"
                |> asFigletText
                |> fun f -> f.Justification |> should equal (System.Nullable Justify.Left))
        }
    ]
