# Status Module

### Example
```fs
let process (context: StatusContext) =
    task {
        do! Task.Delay(3000)
        update "Halfway there ..." context |> ignore

        do! Task.Delay(3000)
        update "Any Moment now ..." context |> ignore

        do! Task.Delay(3000)
    }

(start "Starting up ..." process).Wait()
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Status.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run status example
```