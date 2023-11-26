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
        | ValueDefinition of Name * PropertyType * DefaultValue * Explanation
        | PropDefinition of Name * PropertyType * Explanation

    let setDocumentationStyle =
        selectTheme Documentation
        bulletItemPrefix <- "   >> "
        ()

    let private toPayload artifact = 
        match artifact with
        | FunctionDefinition (Name n, FunctionSignature s) 
            -> P $"{n}: {s}"
        | ValueDefinition (Name n, PropertyType t, DefaultValue d, Explanation e) 
            -> 
            match e.Length with
            | 0 -> Many [ P $"{n}: {t}"; C $"(default:"; P $"{d}"; C")"]
            | _ -> Many [ P $"{n}: {t}"; C $" - {e} (default:"; P $"{d}"; C")"]
        | PropDefinition (Name n, PropertyType t, Explanation e)
            ->  Many [ P $"{n}: {t}"; C $" - {e}"]

    let artifactBullets artifacts = 
        artifacts |> List.map toPayload |> BI

    let private leftAlignedColumn header = 
        column (C header) |> withLayout { defaultColumnLayout with Alignment = Left }

    let artifactTable (artifacts: DocumentationArtifact list) = 
        let columns = 
            match artifacts[0] with
            | FunctionDefinition _ -> 
                [ leftAlignedColumn "Name"
                  leftAlignedColumn "Signature" ]
            | ValueDefinition _ -> 
                [ leftAlignedColumn "Name"
                  leftAlignedColumn "Type"
                  leftAlignedColumn "Default Value"
                  leftAlignedColumn "Explanation" ]
            | PropDefinition _ -> 
                [ leftAlignedColumn "Name"
                  leftAlignedColumn "Type"
                  leftAlignedColumn "Explanation" ]

        artifacts 
        |> List.map (fun articaft -> 
            match articaft with
            | FunctionDefinition (Name n, FunctionSignature s) 
                -> Payloads [P n; P s]
            | ValueDefinition (Name n, PropertyType t, DefaultValue d, Explanation e) 
                -> Payloads [P n; P t; C d; C e]
            | PropDefinition (Name n, PropertyType t, Explanation e) 
                -> Payloads [P n; P t; C e])
        |> customTable { defaultTableLayout with Sizing = Collapse } columns 
        |> toOutputPayload

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
            BL
        ]

    let docMissing = 
        Many [BL; Edgy "Sorry, this documentation is not available yet."] 