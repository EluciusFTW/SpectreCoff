module SpectreCoff.Tests.ThemingTests

open Expecto
open FsUnit.Xunit
open Spectre.Console
open SpectreCoff

[<Tests>]
let themeTests =
  testList "themes" [
    test "iceBergTheme calm uses SteelBlue" { iceBergTheme.calmLook.Color |> should equal (Some Color.SteelBlue) }

    test "volcanoTheme edgy has both Bold and Italic" {
      volcanoTheme.edgyLook.Decorations |> should contain Decoration.Bold
      volcanoTheme.edgyLook.Decorations |> should contain Decoration.Italic
    }

    test "documentationTheme inherits calmLook from iceBergTheme" {
      documentationTheme.calmLook |> should equal iceBergTheme.calmLook
    }

    test "documentationTheme overrides pumpedLook" {
      documentationTheme.pumpedLook |> should not' (equal iceBergTheme.pumpedLook)
    }

    test "documentationTheme edgy has a background color" {
      documentationTheme.edgyLook.BackgroundColor |> should not' (equal None)
    }
  ]
