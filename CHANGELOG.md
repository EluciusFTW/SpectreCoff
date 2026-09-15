# Changelog

Versions follow the scheme `<major>.<minor>.<git-depth>`, where `major.minor` tracks the [Spectre.Console](https://github.com/spectreconsole/spectre.console) dependency being wrapped, and the patch number is the git commit depth — automatically determined by [NerdBank.GitVersioning](https://github.com/dotnet/Nerdbank.GitVersioning). Every merged commit produces a new release on NuGet.

Changelog entries are grouped by `major.minor`. If you are on a specific `0.x.y` version and a change listed under `0.x` is not present, you are on an earlier patch — update to the latest `0.x.*` on NuGet to get it.

## 0.57

Tracks _Spectre.Console_ `0.57.2` (up from `0.54.0`), _Spectre.Console.Cli_ `0.55.0` and _Dumpify_ `0.7.0`. No changes to the _SpectreCoff_ API beyond the breaking change below.

### New features

#### `Figlet`: layout modes
`FigletLayoutMode` decides how tightly letters are packed — `FullSize`, `Fitted` or `Smushed`. It is read from the new `defaultLayoutMode` mutable, and `customFigletWithMode` sets it per call. `figlet` and `customFiglet` keep their signatures.

#### `Table`: cells spanning several columns
The new `Cells of Cell list` case on `Row` carries `Cell of OutputPayload` and `SpanningCell of int * OutputPayload`, where the `int` is how many columns the cell covers. Works for grids as well as tables.

#### `Prompt`: selection prompts with a suggested choice
`chooseFromSuggesting`, `chooseMultipleFromSuggesting` and `chooseMultipleFromSuggestingWith` start the selection on a given choice, the way `askSuggesting` seeds a text prompt. A suggestion that is not among the choices is ignored and the first choice stays highlighted.

#### `Prompt`: cancellable selection prompts
`chooseFromOrCancel`, `chooseMultipleFromOrCancel`, `chooseMultipleFromOrCancelWith`, `chooseGroupedFromOrCancel` and `chooseGroupedFromOrCancelWith` let the user back out of a selection with `Escape`, returning `None` instead of a choice. Cancelling is distinct from choosing nothing — an optional prompt confirmed without a selection still yields `Some []`.

### Breaking changes

#### `Prompt`: two new fields on `PromptOptions`
`PromptOptions` gained `EditableSuggestion` and `ClearOnFinish`, both defaulting to `false`. Code that constructs a `PromptOptions` literally needs the two extra fields; code that builds on `defaultOptions` with a `with` expression is unaffected.

`EditableSuggestion` pre-fills the suggestion of `askSuggesting` into the input so it can be edited in place instead of being accepted wholesale or retyped. `ClearOnFinish` removes the prompt from the console once answered.

#### `Table`: `TableLayout.Alignment` removed
_Spectre.Console_ removed table-level alignment (`Table.LeftAligned()` / `RightAligned()` / `Centered()`) in `0.55`, so the `Alignment` field on `TableLayout` has been dropped rather than left as a field that silently does nothing. Any code constructing a `TableLayout` with `Alignment = ...` needs that field removed. Per-column alignment via `ColumnLayout.Alignment` is unaffected and is now the only way to align table content.

## 0.54

### Breaking changes

#### `Table`: `withLayouts` and `withFooters` parameter types corrected
The inferred types of the parameters were swapped due to incorrect tuple destructuring. Any call site that relied on the (broken) swapped types will need to be updated to pass `layouts: ColumnLayout list` and `columns: ColumnDefinition list` in the correct order.
