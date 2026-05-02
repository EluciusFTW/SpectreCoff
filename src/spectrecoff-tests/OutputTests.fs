module SpectreCoff.Tests.OutputTests

open Expecto
open FsUnit.Xunit
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

    test "NextLine produces empty string" { NextLine |> toMarkedUpString |> should equal "" }

    test "BlankLine produces a single space" { BlankLine |> toMarkedUpString |> should equal " " }

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
