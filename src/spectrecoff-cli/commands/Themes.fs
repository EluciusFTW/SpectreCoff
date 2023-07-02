namespace SpectreCoff.Cli.Commands

open Spectre.Console.Cli
open SpectreCoff
open Microsoft.FSharp.Reflection
open System.ComponentModel

type ThemeSettings() =
    inherit CommandSettings()

    [<CommandOption("-t | --theme")>]
    [<Description("The name of the theme")>]
    member val themeName = "" with get, set

type ListThemes() =
    inherit Command<ThemeSettings>()
    interface ICommandLimiter<ThemeSettings>

    override _.Execute(_context, _) =
        C "The following themes are currently available in SpectreCoff:" |> toConsole
    
        FSharpType.GetUnionCases typeof<SpectreCoffThemes>
        |> Array.map (fun theme -> P theme.Name)
        |> Array.toList
        |> BulletItems
        |> toConsole
        0

type ThemeExample() = 
    inherit Command<ThemeSettings>()

    let printExample theme =
        selectTheme theme
        Many [
            C "The calm fox"
            P "jumps pumped"
            E "over the edgy fence"
        ] |> toConsole

    interface ICommandLimiter<ThemeSettings>
    
    override _.Execute(_context, settings) =
        let matchingTheme = 
            FSharpType.GetUnionCases typeof<SpectreCoffThemes>
            |> Array.tryFind (fun theme -> theme.Name = settings.themeName)
            |> Option.map (fun case -> Reflection.FSharpValue.MakeUnion(case, [||] ) :?> SpectreCoffThemes)

        match matchingTheme with 
            | Some theme -> printExample theme
            | None -> (E $"The theme {settings.themeName} does not exist") |> toConsole
        0
