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

open SpectreCoff.Cli.Documentation

type JsonDocumentation() =
    inherit Command<JsonSettings>()
    interface ICommandLimiter<JsonSettings>

    override _.Execute(_context, _settings) =
        setDocumentationStyle
        Many [
            spectreDocSynopsis "Json module" "This module provides functionality from the json widget of Spectre.Console" "widgets/Json"
            BL
            C "Json can be rendered using the the json function:"
            funcsOutput [
                { Name = "json"; Signature = "(content: string) -> OutputPayload" }
            ]
            BL
            C "The output can be styled by mutation the value of the following variables:"
            valuesOutput [
                { Name = "bracesLook"; Type = "Look"; DefaultValue = "Color: calmColor"; Explanation = "" }
                { Name = "bracketsLook"; Type = "Look"; DefaultValue = "Color: calmColor"; Explanation = "" }
                { Name = "colonLook"; Type = "Look"; DefaultValue = "Color: calmColor"; Explanation = "" }
                { Name = "commaLook"; Type = "Look"; DefaultValue = "Color: calmColor"; Explanation = "" }
                { Name = "memberLook"; Type = "Look"; DefaultValue = "Color: pumpedColor, Decorations: Italic"; Explanation = "" }
                { Name = "stringLook"; Type = "Look"; DefaultValue = "Color: edgyColor, Decorations: Bold"; Explanation = "" }
                { Name = "numberLook"; Type = "Look"; DefaultValue = "Color: edgyColor, Decorations: Bold"; Explanation = "" }
                { Name = "booleanLook"; Type = "Look"; DefaultValue = "Color: calmColor, Decorations: Bold"; Explanation = "" }
                { Name = "nullLook"; Type = "Look"; DefaultValue = "Color: calmColor, Decorations: Dim"; Explanation = "" }
            ]
        ] |> toConsole
        0