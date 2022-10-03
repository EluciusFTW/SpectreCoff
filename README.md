# Spectre.Fs
A thin, opinionated wrapper around [Spectre.Console](https://github.com/spectreconsole/spectre.console) in F#.

## Spectre.Fs
### Goals and Philosophy
Our goal with Spectre.Fs is two-fold: 
* Make Spectre.Console available for console application in F# in an idiomatic way, and moreover
* Provide a very easy, natural and seamless api surface to the underlying functionality of Spectre.

In order to achieve the latter, we are taking a highly opinionated approach. For example, we utilize and support three main styles, _standard_, _emphasized_ and _warn_ that are applied consistently across the different modules. Most modules have one _main style/configuration_, for which specialized functions with the standard style are baked in, but there are other functions to customize the style that require more arguments. All the standards can be configured.

Note that not all functionality and fine-grained configurability of Spectre.Console is exposed. We beleive that we have chosen the most important use-cases of each of the modules. If you do need more specific functionality, you can always implement it on your own. Feel free to browse our source code to get inspired!  

### Documentation
You can find all modules and exposed functions documented below in the section on the api. Additionally, the repository includes a sample cli project (see below), which demonstrates how to consume the api. The api can be used to discover the api and read the documentation as well: For each module, the api has an analogous command, with two sub-commands, _doc_ and _example_. E.g., in order to explore the _table module_,

```PS
dotnet run table doc
```
will output the documentation for the table module, and 
```PS
dotnet run table example
```
will showcase the features of the table module in one example.

### The Spectre.Fs api
Spectre.Fs is organized in modules which mirror the features of Spectre.Console. The source code for the nuget package can be found in the subfolder `/src/spectre-fs/`.

#### Output and Markup

#### Table

#### Prompt

#### Rule

## Sample Cli
Additionally to the package, this repository contains a sample console project demonstrating how to use each of the provided wrappers.
You can see each command in action by navigating to `/src/sample-api/` and running

```PS
dotnet run <command> 
```
or compiling the cli and running the exe.

The currently supported commands, also discoverable by `dotnet run -- -h` (the funky dashes are needed, else dotnet run consumes the arguments!) are
* `greet`, taken over from the starter template, to be renamed
* `table` 
* `prompt`
* `rule`

## Feedback and Contributing
All feedback welcome!
All contributions are welcome!
