module SpectreCoff.Dumpify

open Dumpify

type DumpifyOptions =
    { Members: MembersConfig Option
      Color: ColorConfig Option
      Output: OutputConfig Option
      Table: TableConfig Option
      TypeNames: TypeNamingConfig Option
      UseDescriptors: bool }

let mutable defaultOptions =
    { Members = None
      Color = None
      Output = None
      Table = None
      TypeNames = None
      UseDescriptors = false }

let private getValueOrNull (opt: 'a option) : 'a =
    match opt with
    | Some value -> value
    | None -> null

let dump obj = obj.Dump()

let customDump options obj =
    obj.Dump(
        members = getValueOrNull options.Members,
        colors = getValueOrNull options.Color,
        outputConfig = getValueOrNull options.Output,
        tableConfig = getValueOrNull options.Table,
        typeNames = getValueOrNull options.TypeNames,
        useDescriptors = options.UseDescriptors)
