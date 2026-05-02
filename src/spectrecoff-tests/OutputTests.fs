module SpectreCoff.Tests.OutputTests

open Expecto
open FsUnit.Xunit
open Spectre.Console
open SpectreCoff

[<Tests>]
let markupTests =
  testList "markup" [
    test "empty style returns escaped content" { "hello" |> markup "" |> should equal "hello" }

    test "non-empty style wraps content in markup tags" { "hello" |> markup "bold" |> should equal "[bold]hello[/]" }

    test "content with markup characters is escaped" { "he[llo" |> markup "bold" |> should equal "[bold]he[[llo[/]" }
  ]

[<Tests>]
let toMarkedUpStringTests =
  testList "toMarkedUpString" [
    test "Raw passes content through unchanged" { Raw "hello" |> toMarkedUpString |> should equal "hello" }

    test "Vanilla escapes content but adds no style" { Vanilla "hello" |> toMarkedUpString |> should equal "hello" }

    test "NextLine produces empty string because the newline comes from the rendering layer joining items" {
      NextLine |> toMarkedUpString |> should equal ""
    }

    test "BlankLine produces a single space so it survives markup rendering, unlike an empty string" {
      BlankLine |> toMarkedUpString |> should equal " "
    }

    test "Emoji without colons gets wrapped in colons" { Emoji "smile" |> toMarkedUpString |> should equal ":smile:" }

    test "Emoji already starting with colon is left unchanged" {
      Emoji ":smile:" |> toMarkedUpString |> should equal ":smile:"
    }

    test "Many joins payloads with a space" {
      Many [ Raw "hello"; Raw "world" ]
      |> toMarkedUpString
      |> should equal "hello world"
    }
  ]

[<Tests>]
let isStringifyableTests =
  testList "isStringifyable" [
    testList "returns true for" [
      test "Vanilla" { Vanilla "mango" |> isStringifyable |> should equal true }
      test "Raw" { Raw "papaya" |> isStringifyable |> should equal true }
      test "Calm" { Calm "dragonfruit" |> isStringifyable |> should equal true }
      test "Pumped" { Pumped "kumquat" |> isStringifyable |> should equal true }
      test "Edgy" { Edgy "durian" |> isStringifyable |> should equal true }
      test "Emoji" { Emoji "pineapple" |> isStringifyable |> should equal true }
      test "Link" { Link "https://starfruit.example" |> isStringifyable |> should equal true }
    ]

    testList "returns false for" [
      test "NextLine" { NextLine |> isStringifyable |> should equal false }
      test "BlankLine" { BlankLine |> isStringifyable |> should equal false }
      test "Many" { Many [] |> isStringifyable |> should equal false }
      test "BulletItems" { BulletItems [] |> isStringifyable |> should equal false }
    ]
  ]

[<Tests>]
let markupStringTests =
  testList "markupString" [
    test "color only produces rgb markup" {
      "tangerine"
      |> markupString (Some(Color(200uy, 100uy, 50uy))) []
      |> should equal "[rgb(200,100,50)]tangerine[/]"
    }

    test "decoration only produces decoration markup" {
      "lychee"
      |> markupString None [ Decoration.Bold ]
      |> should equal "[Bold]lychee[/]"
    }

    test "color and decoration are combined" {
      "persimmon"
      |> markupString (Some(Color(10uy, 20uy, 30uy))) [ Decoration.Italic ]
      |> should equal "[rgb(10,20,30) Italic]persimmon[/]"
    }
  ]

[<Tests>]
let markupLinkTests =
  testList "markupLink" [
    test "empty label uses link as content" {
      "https://starfruit.example"
      |> markupLink ""
      |> should haveSubstring "https://starfruit.example"
    }

    test "empty label renders link attribute in style" {
      "https://starfruit.example" |> markupLink "" |> should haveSubstring " link]"
    }

    test "label becomes the content" {
      "https://papaya.example"
      |> markupLink "click me"
      |> should haveSubstring "click me"
    }

    test "label renders link= attribute in style" {
      "https://papaya.example"
      |> markupLink "click me"
      |> should haveSubstring "link=https://papaya.example"
    }
  ]

[<Tests>]
let reduceRenderablesTests =
  testList "reduceRenderables" [
    test "empty list returns empty list" { [] |> reduceRenderables |> should haveLength 0 }

    test "single element is unchanged" { [ Raw "mango" ] |> reduceRenderables |> should equal [ Raw "mango" ] }

    test "two adjacent stringifyables are merged" {
      [ Raw "kiwi"; Raw "lime" ]
      |> reduceRenderables
      |> should equal [ Raw "kiwi lime" ]
    }

    test "three adjacent stringifyables are all merged" {
      [ Raw "kiwi"; Raw "lime"; Raw "fig" ]
      |> reduceRenderables
      |> should equal [ Raw "kiwi lime fig" ]
    }

    test "two non-stringifyables are kept separate" {
      [ NextLine; BlankLine ]
      |> reduceRenderables
      |> should equal [ NextLine; BlankLine ]
    }

    test "stringifyable before non-stringifyable is kept separate" {
      [ Raw "mango"; NextLine ]
      |> reduceRenderables
      |> should equal [ Raw "mango"; NextLine ]
    }

    test "non-stringifyable before stringifyable is kept separate" {
      [ NextLine; Raw "mango" ]
      |> reduceRenderables
      |> should equal [ NextLine; Raw "mango" ]
    }

    test "stringifyables are not merged across a non-stringifyable" {
      [ Raw "kiwi"; NextLine; Raw "lime" ]
      |> reduceRenderables
      |> should equal [ Raw "kiwi"; NextLine; Raw "lime" ]
    }
  ]
