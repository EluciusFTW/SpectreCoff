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
            CO [ 
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

        NewLine |> toConsole
        pumped "Json module"
        |> alignedRule Left
        |> toConsole

        Many [
            CO [
                C "This module provides functionality from the json widget of Spectre.Console ("
                Link "https://spectreconsole.net/widgets/Json"
                C ")"
            ]
            NL
            C "Json can be rendered using the the json function:"
            BI [P "json: (content: string) -> OutputPayload"]
            NL
            C "The output can be styled by mutation the value of the following variables (the default value is given in braces):"
            BI [
                CO [ P "bracesColor"; C "(calmColor)" ]
                CO [ P "bracesDecorations"; C "([ Decoration.None ])" ]
                CO [ P "bracketsColor"; C "(calmColor)" ]
                CO [ P "bracketsDecorations"; C "([ Decoration.None ])" ]
                CO [ P "colonColor"; C "(calmColor)" ]
                CO [ P "colonDecorations"; C "([ Decoration.None ])" ]
                CO [ P "commaColor"; C "(calmColor)" ]
                CO [ P "commaDecorations"; C "([ Decoration.None ])" ]
                CO [ P "memberColor"; C "(pumpedColor)" ]
                CO [ P "memberDecorations"; C "([ Decoration.Italic ])" ]
                CO [ P "stringColor"; C "(edgyColor)" ]
                CO [ P "stringDecorations"; C "([ Decoration.Bold ])" ]
                CO [ P "numberColor"; C "(edgyColor)" ]
                CO [ P "numberDecorations"; C "([ Decoration.Bold ])" ]
                CO [ P "booleanColor"; C "(calmColor)" ]
                CO [ P "booleanDecorations"; C "([ Decoration.Bold ])" ]
                CO [ P "nullColor"; C "(calmColor)" ]
                CO [ P "nullDecorations"; C "([ Decoration.Dim; Decoration.Strikethrough ])" ]
            ]
            NL
        ] |> toConsole
        0