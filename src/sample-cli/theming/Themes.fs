namespace SpectreFs.Sample
open SpectreFs.Output

module Theme = 

    let setDocumentationStyle = 
        bulletItemPrefix <- "   >> "
        emphasizeColor <- "yellow"
        emphasizeStyle <- "italic"
        ()