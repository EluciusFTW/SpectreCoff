namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff

type JsonSettings()  =
    inherit CommandSettings()

type JsonExample() =
    inherit Command<JsonSettings>()
    interface ICommandLimiter<JsonSettings>

    override _.Execute(_context, _settings) =

        Many [
            Many [
                Emoji "backhand_index_pointing_right"
                P "This example is taken directly from Spectre.Console"
                Emoji "astonished_face"
            ]
        ] |> toConsole

        let content =
            """
            {
                "hello": 32,
                "world": {
                    "foo": 21,
                    "bar": true,
                    "baz": [
                        0.32, 0.33e-32,
                        0.42e32, 0.55e+32,
                        {
                            "hello": "world",
                            "lol": null
                        }
                    ]
                }
            }
            """

        json content
        |> panel " Some JSON in a panel "
        |> toConsole

        0
