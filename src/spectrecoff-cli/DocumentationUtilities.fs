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


    let docHeader title = 
        Many[ BL; alignedRule Left (pumped title)]

    let docSynopsis title explanation relativeLink = 
        Many [
            docHeader title
            BL
            C explanation 
            NL
            C"   => "; Link $"https://spectreconsole.net/{relativeLink}" 
            BL
            emptyRule
        ]

    let docMissing = 
        Many [BL; Edgy "Sorry, this documentation is not available yet."] 