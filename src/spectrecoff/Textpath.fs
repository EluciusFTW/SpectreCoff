[<AutoOpen>]
module SpectreCoff.Textpath

open Spectre.Console
open SpectreCoff.Layout
open SpectreCoff.Output

type TextPathOptions = 
    { Border: TableBorder;
      Sizing: SizingBehaviour;
      HideHeaders: bool;
      Alignment: Alignment }

let path content = 
    TextPath content
