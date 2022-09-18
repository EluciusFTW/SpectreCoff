namespace SpectreFs.Commands

open Spectre.Console.Cli
open SpectreFs.Table

type PromptExampleSettings()  =
    inherit CommandSettings()

type PromptExample() =
    inherit Command<TableExampleSettings>()
    interface ICommandLimiter<TableExampleSettings>

    override _.Execute(_context, _) = 
        0