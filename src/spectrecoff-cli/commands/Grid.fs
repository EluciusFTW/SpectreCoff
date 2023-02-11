namespace SpectreCoff.Cli.Commands

open Spectre.Console
open Spectre.Console.Cli
open SpectreCoff

type GridSettings()  =
    inherit CommandSettings()

type GridExample() =
    inherit Command<GridSettings>()
    interface ICommandLimiter<GridSettings>

    override _.Execute(_context, _settings) =
        let numbersGrid = grid [
            Numbers [1; 2]
            Strings ["One"; "Two"; "Three"]
        ]
        Many [
            C "The grid will have as many columns as are needed to accomodate the longest row:"
            numbersGrid.toOutputPayload
        ] |> toConsole

        addRowToGrid numbersGrid  (Numbers [3; 4; 5])
        Many [
            C "Rows can later be added to an existing grid. Keep in mind that the number of elements per row must not exceed the number of columns:"
            numbersGrid.toOutputPayload
        ] |> toConsole

        let renderableGrid = grid [
            Renderables [numbersGrid; numbersGrid]
            Payloads [Emoji Emoji.Known.SmilingFace; Emoji Emoji.Known.SmilingFace]
        ]
        Many [
            C "Aside from strings and numbers, grids can also contain OutputPayloads or other Renderables:"
            renderableGrid.toOutputPayload
        ] |> toConsole
        0

type GridDocumentation() =
    inherit Command<GridSettings>()
    interface ICommandLimiter<GridSettings>

    override _.Execute(_context, _settings) =
        Cli.Theme.setDocumentationStyle

        NewLine |> toConsole
        pumped "Grid module"
        |> alignedRule Left
        |> toConsole

        Many [
            CO [
                C "This module provides functionality from the Grid widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/Grid"
                C ")"
            ]
            NL
            C "The grid can be used by the grid function:"
            BI [
                P "row: Row list -> Grid"
            ]
            NL
            C "Each row is a collection of one of these DUs:"
            BI [
                CO [P "Payloads"; C "of"; P "OutputPayload list"]
                CO [P "Renderables"; C "of"; P "IRenderable list"]
                CO [P "Strings"; C "of"; P "string list"]
                CO [P "Numbers"; C "of"; P "int list"]
            ]
            NL
            C "The number of columns is automatically set to the number of elements in the longest row."
            C "Rows can be added later to the created grid, but the number of their elements must not exceed the number of columns of the grid."
        ] |> toConsole
        0