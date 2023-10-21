namespace SpectreCoff.Cli

open SpectreCoff.Output
open SpectreCoff.Theming

module Documentation =
    open SpectreCoff
    let setDocumentationStyle =
        selectTheme Documentation
        bulletItemPrefix <- "   >> "
        ()

    let DI term explanation =
        Many [ P term; C explanation]

    let documentationHeader title = 
        Many[ BL; alignedRule Left (pumped title)]
