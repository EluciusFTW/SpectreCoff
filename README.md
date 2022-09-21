# Spectre.Fs
A thin, opinionated wrapper around [Spectre.Console](https://github.com/spectreconsole/spectre.console) in F#.

## Spectre.Fs api
The source code for the nuget package can be found in the subfolder `/src/spectre-fs/`. It provides convenient functions, idiomatic to the F# codeing style, around the C# api of Spectre.

To find out what is currently supported, please see the sources, or try the sample cli below!

## Sample Cli
Additionally to the package, this repository contains a sample console project demonstrating how to use each of the provided wrappers.
You can see each command in action by navigating to `/src/sample-api/` and running

```PS
dotnet run <command>
````
or compiling the cli and running the exe.

The currently supported commands, also discoverable by `dotnet run -- -h` (the funky dashes are needed, else dotnet run consumes the arguments!) are
* `greet`, taken over from the starter template, to be renamed
* `table` 
* `prompt`

## Feedback and Contributing
All feedback welcome!
All contributions are welcome!
