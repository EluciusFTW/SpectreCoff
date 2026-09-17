# SpectreCoff

A thin, opinionated F# wrapper around [Spectre.Console](https://spectreconsole.net).

## Project structure

```
src/
  spectrecoff/           # The library (NuGet package: EluciusFTW.SpectreCoff)
  spectrecoff-cli/       # Demo/documentation CLI that exercises every module
  spectrecoff-tests/     # Unit tests (Expecto)
```

## Running tests

```bash
dotnet run --project src/spectrecoff-tests
# or
dotnet test  # once an xUnit/NUnit adapter is added — for now use dotnet run
```

## Testing approach

Tests live in `src/spectrecoff-tests/` and use [Expecto](https://github.com/haf/expecto) with [FsUnit.Xunit](https://github.com/fsprojects/FsUnit) for assertions.

**What to test:** pure functions that transform data, and anything that builds a _Spectre.Console_ object — assert on the object's own properties (`table.Rows`, `figletText.LayoutMode`, `cell.ColumnSpan`) rather than on rendered text.  
**What not to test:** anything that calls `AnsiConsole.*` directly — those require a real terminal.

Tests that swap a global, such as the `default*` mutables in `FigletTests`, must go in a `testSequencedGroup`, since Expecto runs test lists in parallel.

### Test style

- Pipe all the way — no wrapping in parens just to pass a value to a function:
  ```fsharp
  // good
  Raw "hello" |> toMarkedUpString |> should equal "hello"
  "hello" |> markup "bold" |> should equal "[bold]hello[/]"

  // avoid
  toMarkedUpString (Raw "hello") |> should equal "hello"
  ```
- No placeholder strings like `"x"` — use fruits, funny words, or other varied values.
- No extra spaces for vertical alignment — only structurally required whitespace.
- Use `should haveSubstring "..."` for string containment — `should contain` iterates chars.
- Use `should haveLength 0` for empty collection checks — `should equal []` fails on unresolved empty lists, and so does `should equal (Some [])`. For an option around an empty collection, assert `Option.isSome` and `Option.get |> should haveLength 0` separately.

### Covered so far

| Module | Functions tested |
|--------|-----------------|
| `Styling` | `toSpectreStyle` |
| `Output` | `markup`, `markupString`, `markupLink`, `toMarkedUpString`, `isStringifyable`, `reduceRenderables` |
| `Table` | `column`, `withLayout`, `withLayouts`, `withSameLayout`, `withFooter`, `withFooters`, `table` and `grid` with spanning cells |
| `Figlet` | `figlet`, `customFiglet`, `customFigletWithMode`, `defaultLayoutMode` |
| `Theming` | the built-in themes and their inheritance |

### Planned next

- Per-module tests for the remaining pure builder functions (e.g. `Rule`, `Panel`, `Tree`, `Calendar`)
- The `Prompt` module, which needs a headless console to drive the prompts

## Branching conventions

Active refactoring branches are prefixed `refactor/`. Feature work uses `feature/`.
