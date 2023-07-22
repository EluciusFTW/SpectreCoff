namespace SpectreCoff.Cli

open SpectreCoff.Output
open SpectreCoff.Theming

module Theme =
    let setDocumentationStyle =
        selectTheme Documentation
        bulletItemPrefix <- "   >> "
        ()