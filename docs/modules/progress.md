# Progress Module
This module provides functionality from the [progress] element (https://spectreconsole.net/live/progress) of _Spectre.Console_.

A progress can be started by calling the `start` or `startCustom` function,
```fs
start: ProgressOperation -> Task<unit>
startCustom: ProgressTemplate -> ProgressOperation -> Task<unit>
```

where the `ProgressOperation` is a type alias:
```fs
type ProgressOperation = ProgressContext -> Task<unit>
```

The default process template is used by the `start` function, while a custom template instance can be passed to the `startCustom` function. Since _Spectre.Console_ provides some standardized _columns_ in the templates, the package includes an `emptyTemplate` as well, and some builder functions to add the existing columns:
```fs
let emptyTemplate: ProcessTemplate =
    { AutoClear = true
      AutoRefresh = true
      HideCompleted = false
      Columns = [] }

let mutable defaultTemplate =
    emptyTemplate
    |> withDescriptionColumn
    |> withProgressBarColumn
    |> withPercentageColumn
```
All builder funtions have the signature `ProcessTemplate -> ProcessTemplate`. There are two more ones besides teh ones used in the default template: `withRemainingTimeColumn`, `withSpinnerColumn`.

Inside the passed ProgressOperation, _tasks_ can be added to the `ProgressContext` using the `realizeIn` function:
```fs
realizeIn: ProgressContext -> TaskTemplate -> Spectre.Console.ProgressTask
```

The task template is a discriminated union,
```fs
type TaskTemplate =
    | HotPercentageTask of string
    | ColdPercentageTask of string
    | HotCustomTask of float*string
    | ColdCustomTask of float*string
```
where the percentage tasks are assumed to be done at 100, while the other cases have a `float` parameter for defining at which value they are finished. The `string` parameter is for displaying the task label, and as the name suggests, hot tasks are started directly while cold tasks need to be started manually. Also, for convenience, we provide an increment function, 
```fs
startTask: ProgressTask -> unit
incrementBy: float -> ProgressTask -> ProgressTask
```

### Example
A turtle races a rabbit. The rabbit waits cockily until the turtle is half-way, then tries to catch up.
```fs
let race (context: ProgressContext) =
    task {
        let turtleProgress =
            "Turtle"
            |> HotPercentageTask
            |> realizeIn context
        let rabbitProgress =
            (60.0, "Rabbit")
            |> ColdCustomTask
            |> realizeIn context
        while not context.IsFinished do
            turtleProgress |> incrementBy 5 |> ignore
            if turtleProgress.Value > 50 then
                startTask rabbitProgress
            if rabbitProgress.IsStarted then
                rabbitProgress |> incrementBy 7 |> ignore
            do! Task.Delay(300)
    }

let template =
    emptyTemplate
    |> withDescriptionColumn
    |> withSpinnerColumn
    |> withRemainingTimeColumn
    |> withProgressBarColumn

(race |> startCustom template).Wait()
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Progress.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run progress example
```