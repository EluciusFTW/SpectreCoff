# Layout Module
This module provides functionality from the [layout widget](https://spectreconsole.net/widgets/layout) of _Spectre.Console_.

A layout can be created by passing an identifier to the `layout`
```fs
layout: string -> Layout
```

Layouts can be (de-)composed using:
```fs
splitHorizontally: Layout array -> Layout -> Layout
splitVertically: Layout array -> Layout -> Layout
```

Composed layouts can be further fine-tuned using several functions
```fs
withMinimumWidth: int -> Layout -> Layout
withExplicitWidth: Nullable<int> -> Layout -> Layout
withRatio: int -> Layout -> Layout
```

Corresponding functions are available to manipulate child layouts through their parents using their identifier
```fs
withChildMinimumWidth: string -> int -> Layout -> Layout
withChildExplicitWidth: string -> Nullable<int> -> Layout -> Layout
withChildRatio: string -> int -> Layout -> Layout
```

Layouts can be shown and hidden using
```fs
show: Layout -> Layout
hide: Layout -> Layout
```
Again, corresponding methods to target child layouts are available
```fs
showChild: string -> Layout -> Layout
hideChild: string -> Layout -> Layout
```

A layout's content can be changed using
```fs
setContent: IRenderable -> Layout -> Layout
setChildContent: string -> IRenderable -> Layout -> Layout
```
This is a special case where we use the _Spectre.Console_ interface `IRenderable` directly (which all _Spectre.Console_ widgets implement), instead of an `OutputPayload`. This lets you pass mutable objects like `Table` instances, receiving more rows after being added to the layout, which will have the updated state when the layout is finally printed. If you have a payload already that you want to add to the layout, you can use the mapping function from the `Output` module:
```fs
payloadToRenderable: OutputPayload -> IRenderable
```

Finally, the layout can be mapped to an `OutputPayload` using `toOutputPayload` and be sent to the console via the `toConsole` function.

### Example
```fs
let myLayout =
    layout "Root"
    |> splitHorizontally
        [| layout "upper-child"
           layout "lower-child" 
           |> splitVertically 
               [| layout "left-child" |> withRatio 3 
                  layout "right-child" |> withRatio 2 |] |]
    |> setChildContent "upper-child" someContent
    |> setChildContent "right-child" otherContent

myLayout
|> toOutputPayload
|> toConsole
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Layout.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):

```
dotnet run layout example
```