namespace SpectreCoff.Cli

open Spectre.Console
open SpectreCoff.Output
open SpectreCoff.Layout

module Theme = 

    let setDocumentationStyle = 
        bulletItemPrefix <- "   >> "
        
        pumpedLook <- 
            { Color = Some Color.Yellow
              BackgroundColor = None
              Decorations = [ Decoration.Italic ] }

        edgyLook <- 
            { Color = Some Color.DarkKhaki
              BackgroundColor = Some Color.OrangeRed1
              Decorations = [ Decoration.Italic ] }
        
        ()