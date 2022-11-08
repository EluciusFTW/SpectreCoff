namespace SpectreCoff.Cli

open Spectre.Console
open SpectreCoff.Output
open SpectreCoff.Layout

module Theme = 

    let setDocumentationStyle = 
        bulletItemPrefix <- "   >> "
        pumpedColor <- Color.Yellow
        pumpedStyle <- Italic

        edgyColor <- Color.OrangeRed1
        edgyStyle <- Italic
        ()