# Status Module

### Example
```fs
let operation (context: StatusContext) =
    async {
        do! Async.Sleep 500
        update "Halfway there ..." context |> ignore

        do! Async.Sleep 500
        update "Any Moment now ..." context |> ignore

        do! Async.Sleep 500
        return "Done!"
    }

(Status.start "Starting up ..." operation) 
|> Async.RunSynchronously
|> P |> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Status.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run status example
```