namespace SpectreCoff.Cli

open Spectre.Console
open SpectreCoff.Output
open SpectreCoff.Layout

module Theme = 

    let setDocumentationStyle = 
        bulletItemPrefix <- "   >> "
        
        pumpedLook <- 
            { Color = Color.Yellow
              BackgroundColor = None
              Decorations = [ Decoration.Italic ] }

        edgyLook <- 
            { Color = Color.OrangeRed1
              BackgroundColor = None
              Decorations = [ Decoration.Italic ] }
        
        ()