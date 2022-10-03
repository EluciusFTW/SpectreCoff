﻿namespace SpectreFs.Commands

open Spectre.Console.Cli
open SpectreFs.Rule
open SpectreFs.Output

type RuleExampleSettings()  =
    inherit CommandSettings()

type RuleExample() =
    inherit Command<RuleExampleSettings>()
    interface ICommandLimiter<RuleExampleSettings>

    override this.Execute(_context, _settings) =
        alignedRule Left  $"""{emphasize "Hello"}"""
        rule "Fellow"
        alignedRule Right $"""{warn "Developer"}"""
        emptyRule
        0