module SpectreCoff.Tests.TableTests

open Expecto
open FsUnit.Xunit
open SpectreCoff

let private leftLayout = {
  Alignment = Left
  LeftPadding = 1
  RightPadding = 1
  Wrap = false
}

let private rightLayout = {
  Alignment = Right
  LeftPadding = 3
  RightPadding = 3
  Wrap = true
}

[<Tests>]
let columnTests =
  testList "column" [
    test "sets the header" { column (Raw "banana") |> fun c -> c.Header |> should equal (Raw "banana") }

    test "has no footer by default" { column (Raw "banana") |> fun c -> c.Footer |> should equal None }

    test "uses the default layout" {
      column (Raw "banana")
      |> fun c -> c.Layout |> should equal (Some defaultColumnLayout)
    }
  ]

[<Tests>]
let withLayoutTests =
  testList "withLayout" [
    test "replaces the layout" {
      column (Raw "grape")
      |> withLayout leftLayout
      |> fun c -> c.Layout |> should equal (Some leftLayout)
    }

    test "preserves the header" {
      column (Raw "grape")
      |> withLayout leftLayout
      |> fun c -> c.Header |> should equal (Raw "grape")
    }
  ]

[<Tests>]
let withLayoutsTests =
  testList "withLayouts" [
    test "applies each layout to its corresponding column" {
      let cols = [ column (Raw "fig"); column (Raw "date") ]
      let result = cols |> withLayouts [ leftLayout; rightLayout ]
      result.[0].Layout |> should equal (Some leftLayout)
      result.[1].Layout |> should equal (Some rightLayout)
    }
  ]

[<Tests>]
let withSameLayoutTests =
  testList "withSameLayout" [
    test "applies the same layout to every column" {
      let cols = [ column (Raw "mango"); column (Raw "peach") ]

      cols
      |> withSameLayout leftLayout
      |> List.forall (fun c -> c.Layout = Some leftLayout)
      |> should equal true
    }
  ]

[<Tests>]
let withFooterTests =
  testList "withFooter" [
    test "sets the footer" {
      column (Raw "coconut")
      |> withFooter (Raw "total")
      |> fun c -> c.Footer |> should equal (Some(Raw "total"))
    }

    test "preserves the header" {
      column (Raw "coconut")
      |> withFooter (Raw "total")
      |> fun c -> c.Header |> should equal (Raw "coconut")
    }
  ]

[<Tests>]
let withFootersTests =
  testList "withFooters" [
    test "applies each footer to its corresponding column" {
      let cols = [ column (Raw "lime"); column (Raw "lemon") ]
      let result = cols |> withFooters [ Raw "sum"; Raw "avg" ]
      result.[0].Footer |> should equal (Some(Raw "sum"))
      result.[1].Footer |> should equal (Some(Raw "avg"))
    }
  ]
