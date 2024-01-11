# SpectreCoff
_Spectre Console for F#_ - A thin, opinionated wrapper around [Spectre.Console](https://github.com/spectreconsole/spectre.console) featuring [Dumpify](https://github.com/MoaidHathot/Dumpify).

Available at [Nuget: EluciusFTW.SpectreCoff](https://www.nuget.org/packages/EluciusFTW.SpectreCoff/).

## Table of Contents
- [Goals and Philosophy](#goals-and-philosophy)
- [SpectreCoff Package](#spectrecoff-package)
  * [Output Payloads](#output-payloads)
  * [Markup](#markup)
  * [Convenience Styles](#convenience-styles)
  * [Versioning](#versioning)
- [SpectreCoff Cli](#spectrecoff-cli)
- [Related Work](#related-work)
- [License](#license)
- [Feedback and Contributing](#feedback-and-contributing)

## Goals and Philosophy
Before we get into the details, we'd like to outline our goals and our guiding principles for designing the SpectreCoff api surface.

1. **Make Spectre.Console available for console applications in F# in an idiomatic way.**

    We expose separate functionality in different modules, as functions, with typed arguments instead of generics resp. object-typing. Since many of Spectre's functions can handle multiple different kinds of content that often means wrapping your content in discriminated union type. We believe that the expression of intent as well as the resulting robustness and clarity far outweigh the 'overhead' of wrapping. 

1. **Provide a simple and consistent api surface.** 

    In SpectreCoff, we follow the structure Spectre.Console provides very closely. 
    - Features of Spectre are translated into modules of the same name. 
    - Whenever possible, each module exposes a function producing '_the module thing_' that is of same name as the module. This will be in form of an `OutputPayload`.
    - The special module `Output` (which also defines the type `OutputPayload`), provides the function `toConsole` with which everything can be printed. 

    In the example of the figlet widget of Spectre, which translates into the figlet module, it looks like this:
    ```fs
    "Hello World"    // figlet content
    |> figlet        // main function of the module producing the figlet as an OutputPayload 
    |> toConsole     // toConsole function of the figlet module
    ```
    Of course, for more complex objects, there will be more parameters needed. To achieve this simplicity, the main function uses some defaults (in this example the alignment of the figlet). These defaults can be overwritten 'globally' (as they are just static variables in the module), or passed to other functions taking in more arguments, e.g.,
    ```fs
    "Hello again"
    |> alignedFiglet Left 
    |> toConsole

    // if all your figlets should be left-aligned, you can also set that as the default and use the main figlet function
    defaultAlignment <- Left
    ```
1. **Add a bit of sprinke on top.**

    Spectre is great in providing ways to customize output. We wanted to add a bit on top to make it easier to utilize custom styles consistently throughout applications. Among other things, we decided to include three different semantic levels of output, namely: `calm`, `pumped` and `edgy`, which we also call _convenience styles_. These are supported throughout the modules, and each style can be customized individually.  

1. **Bake the cake and eat it, too.**  

    We want to feel the joy, and pain, of using our api in the fullest. That's why we have included a [cli project](#spectrecoff-cli) in this repository, where we expose the full documentation as well as provide examples for each functionality, using the api itself.
    ```fs
    dotnet run figlet doc            // prints the documentation of the figlet module
    dotnet run figlet example        // shows examples of the module in action
    ```
   
1. **Bonus: Do the same for Dumpify**

   Along the way we also added a separate module wrapping the functionality of Dumpify following the same principles. While the main focus remains with Spectre.Console (hence the name of the package), we do think Dumpify's capabilities are useful and related (it uses Spectre.Console internally, after all) enough to just slap it on top of our package.

## SpectreCoff Package
SpectreCoff is organized in modules which mirror the features of _Spectre.Console_. It also contains an additional module exposing the capabilities of _Dumpify_.
The source code for the nuget package can be found in the subfolder `/src/spectrecoff/`.

### Modules
For a list of all modules available, [see here](docs/modules.md). 
But before checking them out, we advise to read the remainder of this section.

### Output Payloads
An important abstraction in _SpectreCoff_ is the `OutputPayload` type, which is a discriminated union type of all the things that can be sent to the console. As a consequence, any act of producing output in _SpectreCoff_ looks like this:
```fs
payload |> toConsole
```

All modules have functions (often of the same name as the module) to create the respective `OutputPayload`, like in the example from the introduction above:
```fs
"Hello World"    // figlet content
|> figlet        // the function of same name as the module, which creates the OutputPayload 
|> toConsole     // this sends the payload to the console
```

This function uses some reasonable defaults for layout and styling. These are stored in mutable variables in the module and can be globally adjusted by changing their values. For one-time changes, most modules have another create function - prefixed with *custom* - which takes in more parameters adjusting the options, layout and styling of the output.

Some of the more complex _Spectre.Console_ objects are inherently mutable, e.g., a table, where rows might be added or removed after creation. In those cases, the main function returns the _Spectre.Console_ type instead, and provides a `toOutputPayload` function which can be used to map them to a payload just before sending them to the console:

```fs
// This creates a Spectre.Console Table
let exampleTable = table columns rows

// This maps and prints the initial table
exampleTable
|> toOutputPayload
|> toConsole

// This adds another row to the table
addRowToTable exampleTable row

// This prints the table again, including the new row
exampleTable
|> toOutputPayload
|> toConsole
```

Payloads can easily be composed using the `Many` payload and then printed all at once. 
Here is a more comprehensive example (without going into details what each payload means at this point):
```fs
Many [
    MarkupC (Color.Green, "Hello friends,")           // Use any available color
    BlankLine    
    Pumped "Welcome to my party tomorrow night!"      // Use the Pumped convenience style
    BL                                                // short for BlankLine
    C "Please bring ... "                             // short for Calm
    BI [                                              // short for BulletItems
        C "some snacks,"        
        P "some games,"                               // short for Pumped
        E "and some creepy stories!"                  // short for Edgy
    ]
    C "See you "; P "later ... "; NL                  // Mixing list separators to indicate the line is also possible
    Emoji "alien_monster"
] |> toConsole
```
You can find a complete list of all payload types [here](docs/output.md).

### Markup
_Spectre.Console_ provides many possibilities to mark up text, which technically are not grouped into features resp. modules. All of these are also encoded in cases of the `OutputPayload` type, with lot's of helper functions. [See here](output.md) for all details on formatting and styling text output.

### Convenience Styles
Additionally, this package introduces three named _convenience styles_, with which we can easily provide a consistent and semantically meaningful styling across the modules:
`Calm`, `Pumped` and `Edgy`.
```Fs
// Says hello world, emphatically
Pumped "Hello world" |> toConsole
```

The convenience styles can globally be altered by mutating the corresponding variables, e.g.,
```Fs
pumpedLook <- { Color = Color.Yellow; Decorations = [ Decoration.Italic ] }
```

In fact, the package also provides several _themes_ out of the box, which can be selected using the `selectTheme` function and provide a theme from the `SpectreCoffThemes` discriminated union type:
```fs
// A composite payload of all convenience styles
let sample = 
    Many [                          
        Calm "The calm fox"
        Pumped "jumps pumped"
        Edgy "over the edgy fence"
    ]
// sets the theme and prints the sample
let printExampleUsing theme = 
    selectTheme theme               
    sample |> toConsole

printExampleUsing Volcano
printExampleUsing NeonLights
```

You can also build your own custom theme and use it with the `applyTheme` function.

### Encoding issues
**Note**: Several features of Spectre.Console depend on UTF8 Encoding. If you experience unexpected output when handling UTF8 characters check the Spectre.Console [best practices](https://spectreconsole.net/best-practices).

### Versioning
We are using [NerdBank.GitVersioning](https://github.com/dotnet/Nerdbank.GitVersioning) and follow the version scheme: `<major>.<minor>.<git-depth>` for out releases. 

Since this package is a wrapper around _Spectre.Console_, we will synchronize our major and minor versions with the ones of the Spectre dependency we are wrapping.

> <b>Note</b>: In particular, the _third number_ in the version does not have the same meaning as the patches in SemVer. Increments in that number may contain breaking changes, in contrast to patch versions in SemVer.

## SpectreCoff Cli
You can see each module in action by using the cli included in this repository in `/src/spectrecoff-cli/`. 
Simply run
```
dotnet run <command> example | doc
```
for any command with the subcommand `example` or `doc`, depending on if you want to see an example, or the documentation of the command.
The currently supported commands are:

| command     | example | doc | 
|-------------|---------|-----|
| output      | ✅       | ❌   |
| rule        | ✅       | ✅   |
| figlet      | ✅       | ✅   |
| panel       | ✅       | ✅   |
| prompt      | ✅       | ✅   |
| bar         | ✅       | ✅   |
| breakdown   | ✅       | ✅   |
| table       | ✅       | ✅   |
| tree        | ✅       | ✅   |
| calendar    | ✅       | ✅   |
| padder      | ✅       | ✅   |
| grid        | ✅       | ✅   |
| textpath    | ✅       | ✅   |
| json        | ✅       | ✅   |
| canvasimage | ✅       | ✅   |
| canvas      | ✅       | ✅   |
| layout      | ✅       | ✅   |
| progress    | ✅       | ✅   |
| livedisplay | ✅       | ✅   |
| status      | ✅       | ✅   |
| dumpify     | ✅       | ✅   |

## Related Work
In _SpectreCoff_ we take the approach of providing types and functions wrapping the Spectre.Console api. If you prefer dsls via computation expressions, check out this awesome project (hey, even if you don't, check it out anyway!):
- [fs-spectre](https://github.com/galassie/fs-spectre) - 👻💻 Spectre.Console with F# style.

Also, if you want to create a cli using `Spectre.Console.Cli` (recently the cli part was extracted into a separate package), you can use my starter template:
- [fsharp-spectre-console-template](https://github.com/EluciusFTW/fsharp-spectre-console-template) - A minimal starter template for using Spectre.Console.Cli in fsharp

## License
Copyright © Guy Buss, Daniel Muckelbauer

SpectreCoff is provided as-is under the MIT license.
See the LICENSE.md file included in the repository.

## Feedback and Contributing
All feedback welcome!
All contributions are welcome!