namespace SpectreCoff.Cli

open SpectreCoff.Output
open SpectreCoff.Theming

module Documentation =
    open SpectreCoff

    type Name = Name of string
    type FunctionSignature = FunctionSignature of string

    type PropertyType = PropertyType of string
    type DefaultValue = DefaultValue of string
    type Explanation = Explanation of string

    type DocumentationArtifact = 
        | FunctionDefinition of Name * FunctionSignature
        | PropertyDefinition of Name * PropertyType * DefaultValue * Explanation

    let setDocumentationStyle =
        selectTheme Documentation
        bulletItemPrefix <- "   >> "
        ()

    let private toOutputPayload artifact = 
        match artifact with
        | FunctionDefinition (Name n, FunctionSignature s) 
            -> P $"{n}: {s}"
        | PropertyDefinition (Name n, PropertyType t, DefaultValue d, Explanation e) 
            -> Many [ P $"{n}: {t}"; C $"{e} ("; P $"{d}"; C")"]

    let print artifacts = 
        artifacts |> List.map toOutputPayload |> BI

    let define term explanation =
        Many [ P $"{term}:"; C explanation]

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