# Prompt Module
This module provides functionality from the [prompts](https://spectreconsole.net/prompts) of _Spectre.Console_.

 For prompting an answer from the user, the following functions can be used,
```fs
ask<'T>: (question: string) -> 'T
askWith<'T>: (options: PromptOptions) -> string -> 'T
askSuggesting<'T>: (answer: 'T) -> string) -> 'T
askWithSuggesting<'T>: options -> 'T -> string -> 'T
confirm: string -> bool
```
where `confirm` is just the boolean special case of ask. The functions not accepting `options` use the 
```fs
let mutable defaultOptions: PromptOptions = 
    { Secret = false
      Optional = false
      EditableSuggestion = false
      ClearOnFinish = false }
```
while the others accept an instance of `PromptOptions`.

`EditableSuggestion` decides what the suggestion of `askSuggesting` is worth: left `false` the user either accepts it wholesale or retypes the answer from scratch, set to `true` the suggestion is pre-filled into the input and can be edited in place. `ClearOnFinish` removes the prompt from the console once it has been answered, which is useful when the answer is echoed back in some other shape.

For finite selections, there are a few more functions:
```fs
chooseFrom: (choices: string list) -> (question: string) -> string
chooseMultipleFrom: (choices: string list) -> string -> string list
chooseMultipleFromWith: (options: MultiSelectionPromptOptions) -> string list -> string -> string list
```          

Each of these has a cancellable counterpart, which lets the user back out with `Escape` and returns an option instead:
```fs
chooseFromOrCancel: (choices: string list) -> (question: string) -> string option
chooseMultipleFromOrCancel: (choices: string list) -> string -> string list option
chooseMultipleFromOrCancelWith: (options: MultiSelectionPromptOptions) -> string list -> string -> string list option
```

Again there are defaults in play,
```fs
let mutable defaultMultiSelectionOptions: MultiSelectionPromptOptions = 
    { PageSize = 10 }
```

You can also group your choices, allowing you to select a whole group by choosing the parent. This is done by using one of the following functions:

```fs
chooseMultipleGroupedFromWith: MultiSelectionPromptOptions -> ChoiceGroups<'T> -> string -> 'T list
chooseMultipleGroupedFrom: ChoiceGroups<'T> -> string -> 'T list
```

and these are cancellable too:
```fs
chooseGroupedFromOrCancelWith: MultiSelectionPromptOptions -> ChoiceGroups<'T> -> string -> 'T list option
chooseGroupedFromOrCancel: ChoiceGroups<'T> -> string -> 'T list option
```

Note that cancelling is distinct from choosing nothing: an optional prompt that the user confirms without picking anything yields `Some []`, while `None` means they backed out.

Following the now known pattern, the corresponding default options are:
```fs
let mutable defaultGroupedSelectionOptions: GroupedSelectionPromptOptions =
    { PageSize = 10
      Optional = false
      SelectionMode = SelectionMode.Leaf }
```

To define the groups, these types are used:

```fs
type ChoiceGroup<'T> =
    { Group: 'T
      Choices: 'T array }

type ChoiceGroups<'T> =
    { Groups: ChoiceGroup<'T> list
      DisplayFunction: 'T -> string }
```

The `DisplayFunction` defines how to extract the name that should be used in the prompt from `'T`.
Not that `Group` is not of type `string` as you might expect. This mirrors Spectre.Console's and can lead to some awkward group definitions as shown in the examples below.
When using `string` as the generic type, it is recommended you use the `defaultChoiceGroups`, which uses the identity function to extract the prompt name (again, see example below).

### Example
Some random prompts you might throw at your users.
```fs
let fruits = [ "Kiwi"; "Pear"; "Grape"; "Plum" ]

// Select a single fruit
let chosenFruit = 
    "If you had to pick one, which would it be?" 
    |> chooseFrom fruits

// Select multiple fruits
let chosenFruits = 
    "Which all do you actually like?" 
    |> chooseMultipleFrom fruits

// Prompt for an integer
let amount =
    $"How many apples do you want?"
    |> ask<int>

// Prompt for a boolean
let answer = confirm "Are you sure?"

// Select multiple fruits defined as strings
let stringlyTypedFoods = { defaultChoiceGroups with Groups = [ { Group = "Fuits"; Choices = [| "Apple"; "Banana"; "Orange" |] }; { Group = "Berries"; Choices = [| "Blueberry"; "Strawberry" |] } ] }
let stringlyTypedResult = chooseMultipleGroupedFrom stringlyTypedFoods "Choose a combination of fruits and berries"

// Select multiple strongly typed fruits
let stronglyTypedFoods =
    { DisplayFunction = (fun food -> food.Name)
      Groups =
        [ { Group = { Name = "Fruits"; Healthiness = None; Tastiness = None }; Choices = [| { Name = "Apple"; Healthiness = Some 4; Tastiness = Some 5 } |] }
          { Group = { Name = "Berries"; Healthiness = None; Tastiness = None }; Choices = [| { Name = "Blueberry"; Healthiness = Some 4; Tastiness = Some 5 }; { Name = "Strawberry"; Healthiness = Some 8; Tastiness = Some 2 } |] } ]}

let stronglyTypedResult =
    chooseMultipleGroupedFromWith defaultMultiSelectionOptions stronglyTypedFoods "Choose to measure the tastiness and healthiness of your foods"
```

### Cli Example
You can run a [similar example](../../src/spectrecoff-cli/commands/Prompt.fs) using the spectrecoff-cli (in the folder `/src/spectrecoff-cli`):
```fs
dotnet run prompt example
```