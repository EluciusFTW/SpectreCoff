namespace SpectreCoff.Cli

open Spectre.Console
open SpectreCoff.Output
open SpectreCoff.Layout

module Theme = 

    let setDocumentationStyle = 
        bulletItemPrefix <- "   >> "
        emphasizeColor <- Color.Yellow
        emphasizeStyle <- Italic
        ()