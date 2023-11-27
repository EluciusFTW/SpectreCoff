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

    let propsOutput (ps: PropDef list) =
        let columns = 
            [ leftAlignedColumn "Name"
              leftAlignedColumn "Type"
              leftAlignedColumn "Explanation" ]

        ps
        |> List.map (fun d ->  Payloads [P d.Name; C d.Type; C d.Explanation])
        |> customTable { defaultTableLayout with Sizing = Collapse } columns
        |> toOutputPayload
    
    let funcsOutput (fs: FuncDef list) =
        let columns = 
            [ leftAlignedColumn "Name"
              leftAlignedColumn "Signature" ]
              
        fs
        |> List.map (fun f ->  Payloads [P f.Name; C f.Signature])
        |> customTable { defaultTableLayout with Sizing = Collapse } columns
        |> toOutputPayload

    let valuesOutput (vs: ValueDef list) =
        let columns = 
            [ leftAlignedColumn "Name"
              leftAlignedColumn "Type"
              leftAlignedColumn "Default Value"
              leftAlignedColumn "Explanation" ]
              
        vs
        |> List.map (fun v ->  Payloads [P v.Name; C v.Type; C v.DefaultValue; C v.Explanation])
        |> customTable { defaultTableLayout with Sizing = Collapse } columns
        |> toOutputPayload
    
    let define term explanation =
        Many [ P $"{term}:"; C explanation]

    let private docHeader title = 
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