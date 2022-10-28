# SpectreCoff
_Spectre Console for F#_ - A thin, opinionated wrapper around [Spectre.Console](https://github.com/spectreconsole/spectre.console).

> <b>Note</b>: SpectreCoff is as of now still under construction and not yet pubished as a Nuget package. Early, incomplete preview versions will be published soon. 
## Table of Contents
- [Goals and Philosophy](#goals-and-philosophy)
- [SpectreCoff Package](#spectrecoff-package)
  * [Output and Markup](#output-and-markup)
  * [Panel](#panel)
  * [Prompt](#prompt)
  * [Rule](#rule)
  * [Figlet](#rule)
  * [Table](#table)
- [SpectreCoff Cli](#spectrecoff-cli)
- [License](#license)
- [Feedback and Contributing](#feedback-and-contributing)

## Goals and Philosophy
Before we get into the details, we'd like to outline our goals and our guiding principles for designing the SpectreCoff api surface.

1. Make Spectre.Console available for console applications in F# in an idiomatic way.  
    We expose separate functionality in different modules, as functions, with typed arguments instead of generics resp. object-typing. Since many of Spectre's functions can handle multiple different kinds of content that often means wrapping your content in a DU. We believe that the expression of intent as well as the resulting robustness and clarity far outweigh the 'overhead' of wrapping. 

1. Provide a very simple and consistent api surface.  
    In SpectreCoff, we follow the structure Spectre.Console provides very closely. Features of Spectre are translated into modules of the same name. Whenever possible, each module exposes a function producing '_the module thing_' that is of same name as the module, as well as a `toConsole` function, which delivers the instance to the console. E.g., for the rule feature,
    ```fs
    // rule is the function in the Rule module, 
    // taking the string content as argument, producing a rule instance
    // toConsole from the Rule module prints it
    rule "Example"     
    |> toConsole 
    ```
    Of course, for more complex objects, there will be more parameters needed. To achieve this simplicity, this function uses some defaults (in this example the alignment of the rule). These defaults can be overwritten 'globally' (as they are just static variables in the module). The module also exposes functions taking in more arguments as well, e.g.,
    ```fs
    // alignedRule takes an Alignment as a further argument
    alignedRule Left "Example"
    |> toConsole

    // if all your rules should be left-aligned, you can also set that as the default, which is used by rule
    defaultAlignment <- Left
    ```

1. Bake the cake and eat it, too.  
    We want to feel the joy, and pain, of using our api in the fullest. That's why we have included a [cli project](#spectrecoff-cli) in this repository, where we expose the full documentation as well as provide examples for each functionality, using the api itself.
    ```fs
    dotnet run table doc            // prints the documentation of the table module
    dotnet run table example        // shows examples of the module in action
    ```

To achieve even more simplicity and consistency we have also decided to bake in three convencience styles,  called _standard_, _emphasize_ and _warn_ (working titles) which are supported and applied consistently as far as possible across the different modules. These can be modified globally as well, of course.

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
markup (Some Color.Red) (Some Bold) "Hello [World]" |> printMarkedUpInline    
```
However, there is even a more idiomatic way. SpectreCoff exposes a discriminated union type, `OutputPayload`, that handles all kinds of output and can be passed to a single function `toConsole`. The example above, rewritten using this type, would be:
```Fs
MarkupCS (Color.Red, Bold, "Hello [World]") |> toConsole
```
There are three convenience styles that can be used throughout SpectreCoff, namely `Standard`, `Emphasize` and `Warn`. Using these is even simpler:
```Fs
Emphasize "Hello world" |> toConsole
```
The convenience styles can be altered by mutating the corresponding variables, e.g.,
```Fs
emphasizeColor <- Color.Yellow
emphasizeStyle <- Style.Italic
```
Using the `OutputPayload` also enables printing more complex content, as well as printing many lines at once, as you can see in this example,
```Fs
ManyMarkedUp [
    MarkupC (Color.Green, "Hello there,")
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

### Figlet
The figlet module is already usable, just not documented yet. In the meantime, please see the example command for guidance.

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
* `rule`
* `figlet`
* `table` 

## License
See the license file included in the repository.

## Feedback and Contributing
All feedback welcome!
All contributions are welcome!
