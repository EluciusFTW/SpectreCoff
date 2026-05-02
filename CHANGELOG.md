# Changelog

Versions follow the scheme `<major>.<minor>.<git-depth>`, where `major.minor` tracks the [Spectre.Console](https://github.com/spectreconsole/spectre.console) dependency being wrapped, and the patch number is the git commit depth — automatically determined by [NerdBank.GitVersioning](https://github.com/dotnet/Nerdbank.GitVersioning). Every merged commit produces a new release on NuGet.

Changelog entries are grouped by `major.minor`. If you are on a specific `0.x.y` version and a change listed under `0.x` is not present, you are on an earlier patch — update to the latest `0.x.*` on NuGet to get it.

## 0.54

### Breaking changes

#### `Table`: `withLayouts` and `withFooters` parameter types corrected
The inferred types of the parameters were swapped due to incorrect tuple destructuring. Any call site that relied on the (broken) swapped types will need to be updated to pass `layouts: ColumnLayout list` and `columns: ColumnDefinition list` in the correct order.

#### `Status`: `StatusOperation` changed from `Async`-based to `Task`-based
`StatusOperation<'Result>` is now `StatusContext -> Task<'Result>` (was `StatusContext -> Async<'Result>`). `start` and `startWithCustomSpinner` now return `Task<'Result>`. Update operation lambdas from `async { }` to `task { }` and `Async.Sleep` to `Task.Delay`.
