module SpectreCoff.Figlet

open Spectre.Console 
open SpectreCoff.Layout
open SpectreCoff.Output

let mutable defaultAlignment = Center
let mutable defaultColor = emphasizeColor

let customFiglet (alignment: Alignment) (color: Color) content = 
    let figlet = FigletText content
    figlet.Color <- color

    match alignment with
    | Left -> figlet.LeftAligned() |> ignore
    | Center -> figlet.Centered() |> ignore
    | Right -> figlet.RightAligned() |> ignore

    figlet

let figlet = 
    customFiglet defaultAlignment defaultColor 

let toConsole (figlet: FigletText) = 
    figlet |> AnsiConsole.Write