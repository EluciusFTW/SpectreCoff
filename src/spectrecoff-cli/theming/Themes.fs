namespace SpectreCoff.Cli

open SpectreCoff.Output
open SpectreCoff.Theming

module Theme = 

    let setDocumentationStyle = 
        bulletItemPrefix <- "   >> "
        selectTheme Documentation
        ()