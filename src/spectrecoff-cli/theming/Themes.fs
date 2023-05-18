namespace SpectreCoff.Cli

open Spectre.Console
open SpectreCoff.Output
open SpectreCoff.Layout

module Theme = 

    let setDocumentationStyle = 
        bulletItemPrefix <- "   >> "
        
        pumpedLook <- 
            { Color = Color.Yellow
              Decoration = [ Decoration.Italic ] }

        edgyLook <- 
            { Color = Color.OrangeRed1
              Decoration = [ Decoration.Italic ] }
        
        ()