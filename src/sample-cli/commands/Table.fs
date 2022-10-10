namespace SpectreFs.Sample.Commands

open Spectre.Console.Cli
open SpectreFs.Table

type TableExampleSettings()  =
    inherit CommandSettings()

type TableExample() =
    inherit Command<TableExampleSettings>()
    interface ICommandLimiter<TableExampleSettings>

    override _.Execute(_context, _) =

        let columns = ["Firstname"; "Lastname"; "Age"]
        let rows = [
            ["Jacob"; "Josephsson"; "31"]
            ["Tim"; "Turner"; "49"]
            ["Walter"; ""; "72"]
            ["Fred"; "Flintstone" ] 
        ]
        
        print (stable columns rows)

        let numericRows = [ [1; 2]; [31; 42; 53] ]
        print (stable columns numericRows)
        0