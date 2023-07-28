namespace SpectreCoff.Cli.Commands

open Spectre.Console
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
                Emoji ":backhand_index_pointing_right:"
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

type JsonDocumentation() =
    inherit Command<JsonSettings>()
    interface ICommandLimiter<JsonSettings>

    override _.Execute(_context, _settings) =
        Cli.Theme.setDocumentationStyle

        BL |> toConsole
        pumped "Json module"
        |> alignedRule Left
        |> toConsole

        Many [
            Many [
                C "This module provides functionality from the json widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/Json"
                C ")"
            ]
            BL
            C "Json can be rendered using the the json function:"
            BI [P "json: (content: string) -> OutputPayload"]
            BL
            C "The output can be styled by mutation the value of the following variables (the default value is given in braces):"
            BI [
                Many [ P "bracesColor"; C "(calmColor)" ]
                Many [ P "bracesDecorations"; C "([ Decoration.None ])" ]
                Many [ P "bracketsColor"; C "(calmColor)" ]
                Many [ P "bracketsDecorations"; C "([ Decoration.None ])" ]
                Many [ P "colonColor"; C "(calmColor)" ]
                Many [ P "colonDecorations"; C "([ Decoration.None ])" ]
                Many [ P "commaColor"; C "(calmColor)" ]
                Many [ P "commaDecorations"; C "([ Decoration.None ])" ]
                Many [ P "memberColor"; C "(pumpedColor)" ]
                Many [ P "memberDecorations"; C "([ Decoration.Italic ])" ]
                Many [ P "stringColor"; C "(edgyColor)" ]
                Many [ P "stringDecorations"; C "([ Decoration.Bold ])" ]
                Many [ P "numberColor"; C "(edgyColor)" ]
                Many [ P "numberDecorations"; C "([ Decoration.Bold ])" ]
                Many [ P "booleanColor"; C "(calmColor)" ]
                Many [ P "booleanDecorations"; C "([ Decoration.Bold ])" ]
                Many [ P "nullColor"; C "(calmColor)" ]
                Many [ P "nullDecorations"; C "([ Decoration.Dim; Decoration.Strikethrough ])" ]
            ]
            BL
        ] |> toConsole
        0