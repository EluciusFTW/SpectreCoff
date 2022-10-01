namespace SpectreFs.Commands

open Spectre.Console.Cli
open SpectreFs.Rule
open SpectreFs.Output

type RuleExampleSettings()  =
    inherit CommandSettings()

type RuleExample() =
    inherit Command<RuleExampleSettings>()
    interface ICommandLimiter<RuleExampleSettings>

    override this.Execute(_context, _settings) =
        ruleContentLeft $"""{emphasize "Hello"}"""
        ruleContent "Fellow"
        ruleContentRight $"""{warn "Developer"}"""
        ruleEmpty
        0