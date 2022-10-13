namespace SpecteCoff.Cli
open SpecteCoff.Output

module Theme = 

    let setDocumentationStyle = 
        bulletItemPrefix <- "   >> "
        emphasizeColor <- "yellow"
        emphasizeStyle <- "italic"
        ()