namespace SpectreCoff.Cli

open SpectreCoff.Output
open SpectreCoff.Theming

module Documentation =
    open SpectreCoff

    type PropDef =
        { Name: string
          Type: string
          Explanation: string }

    type FuncDef =
        { Name: string
          Signature: string }

    type ValueDef =
        { Name: string
          Type: string
          DefaultValue: string
          Explanation: string }

    let setDocumentationStyle =
        selectTheme Documentation
        bulletItemPrefix <- "   >> "
        ()

    let private leftAlignedColumn header =
        column (C header) |> withLayout { defaultColumnLayout with Alignment = Left }

    let propsOutput (definitions: PropDef list) =
        let columns =
            [ leftAlignedColumn "Name"
              leftAlignedColumn "Type"
              leftAlignedColumn "Explanation" ]

        definitions
        |> List.map (fun definition ->  Payloads [P definition.Name; C definition.Type; C definition.Explanation])
        |> customTable { defaultTableLayout with Sizing = Collapse } columns
        |> toOutputPayload

    let funcsOutput (definitions: FuncDef list) =
        let columns =
            [ leftAlignedColumn "Name"
              leftAlignedColumn "Signature" ]

        definitions
        |> List.map (fun definition ->  Payloads [P definition.Name; C definition.Signature])
        |> customTable { defaultTableLayout with Sizing = Collapse } columns
        |> toOutputPayload

    let valuesOutput (definitions: ValueDef list) =
        let columns =
            [ leftAlignedColumn "Name"
              leftAlignedColumn "Type"
              leftAlignedColumn "Default Value"
              leftAlignedColumn "Explanation" ]

        definitions
        |> List.map (fun definition ->  Payloads [P definition.Name; C definition.Type; C definition.DefaultValue; C definition.Explanation])
        |> customTable { defaultTableLayout with Sizing = Collapse } columns
        |> toOutputPayload

    let define term explanation =
        Many [ P $"{term}:"; C explanation]

    let private docHeader title =
        Many[ BL; alignedRule Left (pumped title)]

    let docSynopsis title explanation absoluteLink =
        Many [
            docHeader title
            BL
            C explanation
            NL
            C"   => "; Link absoluteLink
            BL
            emptyRule
            BL
        ]

    let spectreDocSynopsis title explanation relativeLink =
        docSynopsis title explanation $"https://spectreconsole.net/{relativeLink}"

    let docMissing =
        Many [BL; Edgy "Sorry, this documentation is not available yet."]