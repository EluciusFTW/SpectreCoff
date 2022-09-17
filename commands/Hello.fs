namespace Commands

module Greet =
    open Spectre.Console.Cli
    open Output

    type HelloSettings() as self =
        inherit CommandSettings()

        [<CommandOption("-n|--name")>]
        member val name = "friend" with get, set

        override _.Validate() =
            match self.name.Length with
            | 1 -> Spectre.Console.ValidationResult.Error($"That's an awfully short name, I don't buy it.")
            | _ -> Spectre.Console.ValidationResult.Success()
    
    type Hello() =
        inherit Command<HelloSettings>()
        interface ICommandLimiter<HelloSettings>

        override _.Execute(_context, settings) = 
            printMarkedUp $"Hello {emphasize settings.name}! Enjoy the Spectre.Console starter template for F#."
            0