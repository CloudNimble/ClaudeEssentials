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
| `ConfigChange` | React to configuration changes during a session |
| `CwdChanged` | React when the working directory changes |
| `DirectoryAdded` | React when a directory is added to the session |
| `Elicitation` | Answer an MCP server's request for input |
| `ElicitationResult` | Override the response before it reaches the MCP server |
| `FileChanged` | React when a watched file changes |
| `InstructionsLoaded` | See when CLAUDE.md and rules files load |
| `MessageDisplay` | Change how assistant text appears on screen |
| `Notification` | React to permission prompts and other notifications |
| `PermissionDenied` | React to a denied tool call and optionally retry it |
| `PermissionRequest` | Answer permission dialogs and apply permission updates |
| `PostCompact` | Act on the summary compaction produced |
| `PostModelSwitch` | Add context after a model switch |
| `PostToolBatch` | Act once every tool call in a batch has resolved |
| `PostToolUse` | Add context or replace a tool's output after it completes |
| `PostToolUseFailure` | React when a tool fails |
| `PreCompact` | Customize context compaction |
| `PreModelSwitch` | Allow, deny, or confirm a model switch |
| `PreToolUse` | Approve, deny, or modify tool calls before execution |
| `SessionEnd` | Clean up when sessions end |
| `SessionStart` | Inject project context when sessions begin |
| `Setup` | Run repository setup or maintenance |
| `Stop` | Enforce policies before Claude ends a turn |
| `StopFailure` | React when a turn ends because of an API error |
| `SubagentStart` | Add context when a subagent starts |
| `SubagentStop` | Enforce policies before a subagent finishes |
| `TaskCompleted` | React when a task is completed |
| `TaskCreated` | React when a task is created |
| `TeammateIdle` | React when a teammate is about to go idle |
| `UserPromptExpansion` | Inspect or block slash command and MCP prompt expansions |
| `UserPromptSubmit` | Process prompts before they're sent |
| `WorktreeCreate` | Create a worktree your own way |
| `WorktreeRemove` | Clean up when a worktree is removed |

## Features

- **AOT Ready** - Source-generated serialization, zero reflection
- **Strongly Typed** - Full IntelliSense and compile-time safety
- **Lightweight** - No dependencies beyond System.Text.Json

## Documentation

Full documentation available at **[ai.easyaf.dev/claudeessentials](https://ai.easyaf.dev/claudeessentials)**

## License

MIT
