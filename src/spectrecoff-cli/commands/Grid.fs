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

        (Numbers [3; 4; 5]) |> numbersGrid.addRow
        Many [
            C "Rows can later be added to an existing grid. Keep in mind that the number of elements per row must not exceed the number of columns:"
            numbersGrid.toOutputPayload
        ] |> toConsole

        let renderableGrid = grid [
            Payloads [numbersGrid.toOutputPayload; numbersGrid.toOutputPayload]
            Payloads [Emoji Emoji.Known.SmilingFace; Emoji Emoji.Known.SmilingFace]
        ]
        Many [
            C "Aside from strings and numbers, grids can also contain OutputPayloads:"
            renderableGrid.toOutputPayload
        ] |> toConsole
        0
