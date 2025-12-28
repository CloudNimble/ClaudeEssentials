# ClaudeEssentials

[![NuGet](https://img.shields.io/nuget/v/ClaudeEssentials.svg)](https://www.nuget.org/packages/ClaudeEssentials)
[![Build](https://github.com/CloudNimble/ClaudeEssentials/actions/workflows/build-and-deploy.yml/badge.svg)](https://github.com/CloudNimble/ClaudeEssentials/actions/workflows/build-and-deploy.yml)

AOT-ready .NET models for building [Claude Code](https://docs.anthropic.com/en/docs/claude-code) hook processors.

## What It Does

Claude Code hooks let you intercept and customize Claude's behavior. ClaudeEssentials provides strongly-typed C# models for all hook events, with source-generated JSON serialization for instant startup.

## Installation

```bash
dotnet add package ClaudeEssentials
```

## Quick Example

```csharp
using CloudNimble.ClaudeEssentials.Hooks;
using CloudNimble.ClaudeEssentials.Hooks.Enums;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;

// Read JSON from stdin
var json = Console.In.ReadToEnd();
var input = ClaudeHooksSerializer.DeserializePreToolUseInput(json);

// Auto-approve read-only tools
var output = new PreToolUseHookOutput<object>
{
    Continue = true,
    HookSpecificOutput = new PreToolUseSpecificOutput<object>
    {
        PermissionDecision = input.ToolName is "Read" or "Glob"
            ? PermissionDecision.Allow
            : PermissionDecision.Ask
    }
};

// Write response to stdout
Console.WriteLine(ClaudeHooksSerializer.SerializePreToolUseOutput(output));
```

## Supported Hooks

| Hook | Purpose |
|------|---------|
| `PreToolUse` | Approve, deny, or modify tool calls before execution |
| `PostToolUse` | Add context or take action after tools complete |
| `Stop` | Enforce policies before Claude ends a session |
| `SessionStart` | Inject project context when sessions begin |
| `Notification` | React to permission prompts and notifications |
| `UserPromptSubmit` | Process user prompts before they're sent |
| `PreCompact` | Customize context compaction behavior |

## Features

- **AOT Ready** - Source-generated serialization, zero reflection
- **Strongly Typed** - Full IntelliSense and compile-time safety
- **Lightweight** - No dependencies beyond System.Text.Json

## Documentation

Full documentation available at **[easyaf.dev/claudeessentials](https://easyaf.dev/claudeessentials)**

## License

MIT
