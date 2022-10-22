# SpectreCoff
_Spectre Console for F#_ - A thin, opinionated wrapper around [Spectre.Console](https://github.com/spectreconsole/spectre.console).

> <b>Note</b>: SpectreCoff is as of now still under construction and not yet pubished as a Nuget package. Early, incomplete preview versions will be published soon. 
## Table of Contents
- [Goals and Philosophy](#goals-and-philosophy)
- [Documentation](#documentation)
- [SpectreCoff Package](#spectrecoff-package)
  * [Output and Markup](#output-and-markup)
  * [Panel](#panel)
  * [Prompt](#prompt)
  * [Rule](#rule)
  * [Table](#table)
- [SpectreCoff Cli](#spectrecoff-cli)
- [License](#license)
- [Feedback and Contributing](#feedback-and-contributing)

## Goals and Philosophy
Our goal with SpectreCoff is two-fold: 
* Make Spectre.Console available for console applications in F# in an idiomatic way, and moreover
* Provide a very easy, natural and seamless api surface to the underlying functionality of Spectre.

In order to achieve the latter, we are taking a highly opinionated approach. For example, we utilize and support three main styles, _standard_, _emphasized_ and _warn_ that are applied consistently across the different modules. Most modules have one _main style/configuration_, which are used in the core functions of the module. However, functions accepting customized styles and options are provided as well, but will naturally require more arguments. All the standards can also be modified.

Note that not all functionality and fine-grained configurability of Spectre.Console is exposed. We believe that we have chosen the most important use-cases of each of the modules. If you do need more specific functionality, you can always implement it on your own. Feel free to browse our source code to get inspired!  

## Documentation
You can (eventually) find all modules and exposed functions documented below in the section on the [SpectreCoff package](#spectrecoff-package). Additionally, the repository includes a [cli project](#spectrecoff-cli) (see below), which demonstrates how to consume the api. The api can be used to discover the api and read the documentation as well: For each module, the api has an analogous command, with two sub-commands, _doc_ and _example_. E.g., in order to explore the _table module_,

```PS
dotnet run table doc
```
will output the documentation for the table module, and 
```PS
dotnet run table example
```
will showcase the features of the table module in one example.

## SpectreCoff Package
SpectreCoff is organized in modules which mirror the features of Spectre.Console. The source code for the nuget package can be found in the subfolder `/src/spectrecoff/`.

> <b>Note</b>: Although we speak of package, SpectreCoff is as of now not yet pubished as a Nuget package.

### Output and Markup
Spectre offers very flexible markup by using variations of this command ([see here](https://spectreconsole.net/markup)):
```Cs
AnsiConsole.Markup("[red bold]{0}[/]", Markup.Escape("Hello [World]"));
```
Doing exactly the same in SpectreCoff looks like this:
```Fs
markup "red" "bold" "Hello [World]" |> printMarkedUpInline    
```
However, there is even a more idiomatic way. SpectreCoff exposes a discriminated union type, `OutputPayload`, that handles all kinds of output and can be passed to a single function `toConsole`. The example above, rewritten using this type, would be:
```Fs
MarkupCS ("red", "bold", "Hello [World]") |> toConsole
```
There are three convenience styles that can be used throughout SpectreCoff, namely `Standard`, `Emphasize` and `Warn`. Using these is even simpler:
```Fs
Emphasize "Hello world" |> toConsole
```
The convenience styles can be altered by mutating the corresponding variables, e.g.,
```Fs
emphasizeColor <- "yellow"
emphasizeStyle <- "italic"
```
Using the OutputPayload also enables printing more complex content, as well as printing many lines at once, as you can see in this example,
```Fs
ManyMarkedUp [
    MarkupC ("green", "Hello there,")
    Newline    
    Emphasize "Welcome to my party tomorrow night!"
    NL                                                // short for Newline
    S "Please bring ... "                             // short for Standard
    BI [                                              // short for BulletItems
        S "some snacks,"        
        E "some games,"                               // short for Emphasize
        W "and some creepy stories!"                  // short for Warn
    ]
    NL
    CO [S "See you "; E "later ... "]                 // short for Collection
] |> toConsole
``` 
in which we also illustrate a few other concepts:
* There are control cases, like `Newline` that allow for formatting text blocks in one go,
* All union cases have short abbreviations: `MarkupCS = MCS`, `Newline = NL`, etc. The reason for this is apparent when looking at the block inside BI: By having one letter abbreviations available, the actual content stays aligned and is not distrated from.
* There are complex subconstructs, like `BulletItems = BI` and `Collections = CO`

Similar to `ManyMarkedUp`, there is also `Many`, which takes a list of strings and when sent to the console, prints each string on a separate line, in the _standard style_. 

For a full list of all cases, please check the [source](https://github.com/EluciusFTW/SpectreCoff/blob/main/src/spectrecoff/Output.fs), and for more complete examples the [sample command](https://github.com/EluciusFTW/SpectreCoff/blob/main/src/spectrecoff-cli/commands/Output.fs).

### Panel
The panel module is already usable, just now documented yet. In the meantime, please see the example command for guidance. 

### Prompt
The prompt module is already usable, just now documented yet. In the meantime, please see the example command for guidance. 

### Rule
The rule module is already usable, just now documented yet. In the meantime, please see the example command for guidance.

### Table
The table module is currently in the works!

## SpectreCoff Cli
You can see each module in action by using the cli included in this repository in `/src/spectrecoff-cli/`. Just run 
```PS
dotnet run <command> <example/doc>
```
Each module can (eventually) be used as a command, with the two subcommands `example` and `doc`. The former will showcase the module while the latter can be used to discover the documentation of the module. The currently supported commands are:
* `output`
* `panel`
* `prompt`
* `table` 
* `rule`

## License
See the license file included in the repository.

## Feedback and Contributing
All feedback welcome!
All contributions are welcome!
