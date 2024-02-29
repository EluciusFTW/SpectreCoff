# Output and Markup 
In this document we go into detail of how to customize string output (markup) as a special case of _any output_ (payloads).

## Translating Spectre.Console Markup to SpectreCoff
_Spectre.Console_ offers very flexible markup by using variations of this command ([see here](https://spectreconsole.net/markup)):
```Cs
AnsiConsole.Markup("[red bold]{0}[/]", Markup.Escape("Hello [World]"));
```
There are several ways to achieve the same in _SpectreCoff_. 
The most direct translation looks like this:
```Fs
markup (Some Color.Red) (Some Bold) "Hello [World]" |> printMarkedUpInline    
```
Note that this already hides all the string markup syntax.

However, instead of using this function, we recommend to consistently build an `OutputPayload` instead which cna then be composed, and later printed using the `toConsole` function. The equivalent to the example above would be:
```Fs
MarkupCS (Color.Red, Bold, "Hello [World]") |> toConsole
```
As you can see, the option type dissapeared as well, because we chose `MarkupCS` which requires a color and a style, in contrast to, e.g., `MarkupC` which only accepts a color.

## Output Payloads
In the previous paragraph we saw how to think in terms of payloads. The following table lists all payloads available.

| Type            | Alias | Description                                                       | Parameters                                                                                                  | Configurbility                                                                       |
|-----------------|-------|------------------------------------------------|--------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------|
| `MarkupD`       | `MD`  | Content marked up with decorations                                | decorations: `Spectre.Console.Decoration list`<br /> content: `string`                                      | -                                                                                    |
| `MarkupC`       | `MC`  | Content marked up with a color                                    | color: `Spectre.Console.Color`<br /> content: `string`                                                      | -                                                                                    |
| `MarkupCD`      | `MCD` | Content marked up with a color and decorations                    | decorations: `Spectre.Console.Decoration list`<br /> color: `Spectre.Console.Color`<br /> content: `string` | -                                                                                    |
| `Calm`          | `C`   | Convenience style for calm output                                 | content: `string`                                                                                           | color: `Output.calmLook.Color` <br /> decorations: `Output.calmLook.Decorations`     |
| `Pumped`        | `P`   | Convenience style pumped output                                   | content: `string`                                                                                           | color: `Output.pumpedLook.Color` <br /> decorations: `Output.pumpedLook.Decorations` |
| `Edgy`          | `E`   | Convenience style for edgy output                                 | content: `string`                                                                                           | color: `Output.edgyLook.Color` <br /> decorations: `Output.edgyLook.Decorations`     |
| `Vanilla`       | `V`   | Raw type, no processing will be done, except escaping the content | content: `string`                                                                                           | -                                                                                    |
| `Raw`           | `R`   | Raw type, no processing will be done, no escaping                 | content: `string`                                                                                           | -                                                                                    |
| `NextLine`      | `NL`  | Ends the current line                                             | -                                                                                                           | -                                                                                    |
| `BlankLine`     | `BL`  | Ends the current line and adds an empty line                      | -                                                                                                           | -                                                                                    |
| `Link`          | -     | Clickable link showing the URL                                    | content: `string`                                                                                           | color: `Output.linkLook.Color` <br /> decorations: `Output.linkLook.Decorations`     |
| `LinkWithLabel` | -     | Clickable link showing a label                                    | label: `string` <br /> link: `string`                                                                       | color: `Output.linkLook.Color` <br /> decorations: `Output.linkLook.Decorations`     |
| `Emoji`         | -     | An emoji, given by it's string literal                            | emoji: `string`                                                                                             | -                                                                                    |
| `BulletItems`   | `BI`  | Show list of items with bullet points                             | items: list of `OutputPayload`. <br /> Not allowed: `Renderable`, `BulletItems`                             | bullet item prefix: `Output.bulletItemPrefix`                                        |
| `Many`          | -     | Prints many payloads at once on the same line                     | items: list of `OutputPayload`                                                                              | -                                                                                    |
| `Renderable`    | -     | Wraps a Spectre.Rendering.IRenderable                             | content: `Spectre.Rendering.IRenderable`                                                                    | -                                                                                    |

## Convenience Styles
Recall from [this section of the readme](../README.md#convenience-styles) that _SpectreCoff_ also provides convenience styles, i.e., three named styles that are used in the various modules by default, which can also be customized. Using these consistently reduces the need for a lot of custom markup!

There are places where the api does not accept an `OutputPayload` but only a _marked up string_ (e.g., the text in a _rule_). While in those cases, e.g., `Pumped "My title"` won't work, you can still use the convenience styles consistently with the following functions that produce the string marked up in the corresponding style:
```fs
calm: string -> string
pumped: string -> string
edgy: string -> string
```
