namespace SpectreCoff.Cli
open SpectreCoff.Output

module Theme = 

    let setDocumentationStyle = 
        bulletItemPrefix <- "   >> "
        emphasizeColor <- "yellow"
        emphasizeStyle <- "italic"
        ()