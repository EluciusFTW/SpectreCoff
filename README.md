# SpectreCoff
_Spectre Console for F#_ - A thin, opinionated wrapper around [Spectre.Console](https://github.com/spectreconsole/spectre.console).

## Table of Contents
- [Goals and Philosophy](#goals-and-philosophy)
- [Documentation](#documentation)
- [SpectreCoff Package](#spectrecoff-package)
  * [Output and Markup](#output-and-markup)
  * [Prompt](#prompt)
  * [Rule](#rule)
  * [Table](#table)
- [SpectreCoff Cli](#spectrecoff-cli)
- [License](#license)
- [Feedback and Contributing](#feedback-and-contributing)

## Goals and Philosophy
Our goal with SpectreCoff is two-fold: 
* Make Spectre.Console available for console application in F# in an idiomatic way, and moreover
* Provide a very easy, natural and seamless api surface to the underlying functionality of Spectre.

In order to achieve the latter, we are taking a highly opinionated approach. For example, we utilize and support three main styles, _standard_, _emphasized_ and _warn_ that are applied consistently across the different modules. Most modules have one _main style/configuration_, for which specialized functions with the standard style are baked in, but there are other functions to customize the style that require more arguments. All the standards can be configured.

Note that not all functionality and fine-grained configurability of Spectre.Console is exposed. We beleive that we have chosen the most important use-cases of each of the modules. If you do need more specific functionality, you can always implement it on your own. Feel free to browse our source code to get inspired!  

## Documentation
You can find all modules and exposed functions documented below in the section on the api. Additionally, the repository includes a sample cli project (see below), which demonstrates how to consume the api. The api can be used to discover the api and read the documentation as well: For each module, the api has an analogous command, with two sub-commands, _doc_ and _example_. E.g., in order to explore the _table module_,

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

### Output and Markup
Spectre offers very flexible markup by using variations of this command ([see here](https://spectreconsole.net/markup)):
```Cs
AnsiConsole.Markup("[red bold]{0}[/]", Markup.Escape("Hello [World]"));
```
Doing exactly the same in SpectreCoff looks like this:
```Fs
markup "red" "bold" "Hello [World]" |> printMarkedUp    
```
However, there is even a more idiomatic way. SpectreCoff exposes a DU type, `OutputPayload`, that handles all kinds of output and can be passed to a single function `toConsole`. The example above, rewritten using this type, would be:
```Fs
MarkupCS ("red", "bold", "Hello [World]") |> toConsole
```
While for single-line and single-style printing this does not make a big difference, this allows to print much more complex and composed constructs in one go, like:
```Fs
ManyMarkedUp [
    MarkupC ("green", "Hello there,")
    Newline
    Emphasize "Welcome to my party tomorrow night!"
    NL
    S "Please bring ... "
    BI [
        S "some snacks,"
        E "some games,"
        W "and some creepy stories!"
    ]
    NL
    CO [S "See you "; E "later ... "]
] |> toConsole
``` 
You can see a few new concepts in the example above:
* All union cases have short abbreviations: `MarkupCS = MCS`, `Newline = NL`, ... 
* There are contol cases, like `Newline = NL` that allow for formatting text blocks in one go,
* There are complex subconstructs, like `BulletItems = BI` and `Collections = CO`
* There are three convenience styles, `Emphasize = E`, `Warn = W`, `Standard = S`.

When using the predefined convenience styles, the reason for the abbreviations becomes apparent. Glancing at the bullet items above, one can see that these allow one to use three different styles very easily, _with same indentation_, and hence, the actual content looks very readable.

The convenience styles can be altered by mutating the variables, e.g.,
```Fs
emphasizeColor <- "yellow"
emphasizeStyle <- "italic"
```

Similar to `ManyMarkedUp`, there is also `Many` which just takes a list of strings, and when sent to the console, prints each string on a separate line, in the _standard style_. 

For a full list of all cases, please check the [source](https://github.com/EluciusFTW/SpectreCoff/blob/main/src/spectrecoff/Output.fs), and for more complete examples the [sample command](https://github.com/EluciusFTW/SpectreCoff/blob/main/src/spectrecoff-cli/commands/Output.fs).

### Prompt
The prompt module is already usable, just now documented yet. In the meantime, please see the example command for guidance. 

### Rule
The rule module is already usable, just now documented yet. In the meantime, please see the example command for guidance.

### Table
The table module is currently in the works!

### Others
... to be implemented.

## SpectreCoff Cli
Additionally to the package, this repository contains a console project demonstrating how to use each of the provided wrappers. You can see each command in action by navigating to `/src/spectrecoff-cli/` and running

```PS
dotnet run <command> 
```
or compiling the cli and running the exe.

The currently supported commands, also discoverable by `dotnet run -- -h` (the funky dashes are needed, else dotnet run consumes the arguments!) are
* `output`
* `table` 
* `prompt`
* `rule`

## Licence

## Feedback and Contributing
All feedback welcome!
All contributions are welcome!
