module SpectreCoff.Tests.StylingTests

open Expecto
open FsUnit.Xunit
open Spectre.Console
open SpectreCoff

[<Tests>]
let toSpectreStyleTests =
  testList "toSpectreStyle" [
    test "foreground color is mapped" {
      let result =
        {
          Color = Some(Color(200uy, 100uy, 50uy))
          BackgroundColor = None
          Decorations = []
        }
        |> toSpectreStyle

      result.Foreground |> should equal (Color(200uy, 100uy, 50uy))
    }

    test "background color is mapped" {
      let result =
        {
          Color = None
          BackgroundColor = Some(Color(10uy, 20uy, 30uy))
          Decorations = []
        }
        |> toSpectreStyle

      result.Background |> should equal (Color(10uy, 20uy, 30uy))
    }

    test "absent color maps to Color.Default" {
      let result =
        {
          Color = None
          BackgroundColor = None
          Decorations = []
        }
        |> toSpectreStyle

      result.Foreground |> should equal Color.Default
    }

    test "single decoration is mapped" {
      let result =
        {
          Color = None
          BackgroundColor = None
          Decorations = [ Decoration.Bold ]
        }
        |> toSpectreStyle

      result.Decoration |> should equal Decoration.Bold
    }

    test "multiple decorations are combined" {
      let result =
        {
          Color = None
          BackgroundColor = None
          Decorations = [ Decoration.Bold; Decoration.Italic ]
        }
        |> toSpectreStyle

      result.Decoration |> should equal (Decoration.Bold ||| Decoration.Italic)
    }

    test "empty decoration list maps to Decoration.None" {
      let result =
        {
          Color = None
          BackgroundColor = None
          Decorations = []
        }
        |> toSpectreStyle

      result.Decoration |> should equal Decoration.None
    }
  ]
