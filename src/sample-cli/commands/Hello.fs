namespace SpectreFs.Commands

open Spectre.Console.Cli
open SpectreFs.Output
open SpectreFs.Table

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
        
        let colums = ["first"; "second"; "third"]
        let rows = [ ["a"; "b"; "c"]; ["d"; "e"; "f"] ]
        print (stable colums rows) 

        let numericRows = [ [1; 2]; [31; 42; 53] ]
        print (stable colums numericRows)
        0

type TableExample() =
    inherit Command<CommandSettings>()
    interface ICommandLimiter<CommandSettings>

    override _.Execute(_context, settings) = 
        
        let colums = ["Firstname"; "Lastname"; "Age"]
        let rows = [ ["Jacob"; "Josephsson"; "31"]; ["Tim"; "Turner"; "49"] ]
        print (stable colums rows) 

        let numericRows = [ [1; 2]; [31; 42; 53] ]
        print (stable colums numericRows)
        0