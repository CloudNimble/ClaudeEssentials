using System.Text.Json;
using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Enums;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Provides AOT-compatible JSON serialization context for Claude Code hook types.
    /// This context uses source generators to pre-compile serialization code,
    /// eliminating the need for runtime reflection.
    /// </summary>
    /// <remarks>
    /// <para>
    /// For generic hook types (PreToolUseHookInput, PostToolUseHookInput, etc.),
    /// this context registers versions using <see cref="object"/> as the type parameter.
    /// If you need strongly-typed serialization for specific tool inputs/outputs,
    /// create your own <see cref="JsonSerializerContext"/> with additional type registrations.
    /// </para>
    /// <para>
    /// Usage example:
    /// <code>
    /// var input = JsonSerializer.Deserialize(json, ClaudeHooksJsonContext.Default.PreToolUseHookInputObject);
    /// var output = JsonSerializer.Serialize(hookOutput, ClaudeHooksJsonContext.Default.PreToolUseHookOutputObject);
    /// </code>
    /// </para>
    /// </remarks>
    [JsonSourceGenerationOptions(
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified,
        WriteIndented = false,
        UseStringEnumConverter = true,
        GenerationMode = JsonSourceGenerationMode.Default)]

    // Common types needed for generic object handling
    [JsonSerializable(typeof(JsonElement))]

    // Enums
    [JsonSerializable(typeof(HookEventName))]
    [JsonSerializable(typeof(PermissionMode))]
    [JsonSerializable(typeof(PermissionDecision))]
    [JsonSerializable(typeof(HookDecision))]
    [JsonSerializable(typeof(PermissionRequestBehavior))]
    [JsonSerializable(typeof(SessionStartSource))]
    [JsonSerializable(typeof(SessionEndReason))]
    [JsonSerializable(typeof(NotificationType))]
    [JsonSerializable(typeof(CompactTrigger))]

    // Input types (using object for generic parameters)
    [JsonSerializable(typeof(PreToolUseHookInput<object>), TypeInfoPropertyName = "PreToolUseHookInputObject")]
    [JsonSerializable(typeof(PostToolUseHookInput<object, object>), TypeInfoPropertyName = "PostToolUseHookInputObject")]
    [JsonSerializable(typeof(PermissionRequestHookInput<object>), TypeInfoPropertyName = "PermissionRequestHookInputObject")]
    [JsonSerializable(typeof(NotificationHookInput))]
    [JsonSerializable(typeof(UserPromptSubmitHookInput))]
    [JsonSerializable(typeof(StopHookInput))]
    [JsonSerializable(typeof(SubagentStopHookInput))]
    [JsonSerializable(typeof(PreCompactHookInput))]
    [JsonSerializable(typeof(SessionStartHookInput))]
    [JsonSerializable(typeof(SessionEndHookInput))]

    // Output types (using object for generic parameters)
    [JsonSerializable(typeof(PreToolUseHookOutput<object>), TypeInfoPropertyName = "PreToolUseHookOutputObject")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<object>), TypeInfoPropertyName = "PreToolUseSpecificOutputObject")]
    [JsonSerializable(typeof(PermissionRequestHookOutput<object>), TypeInfoPropertyName = "PermissionRequestHookOutputObject")]
    [JsonSerializable(typeof(PermissionRequestSpecificOutput<object>), TypeInfoPropertyName = "PermissionRequestSpecificOutputObject")]
    [JsonSerializable(typeof(PermissionRequestDecision<object>), TypeInfoPropertyName = "PermissionRequestDecisionObject")]
    [JsonSerializable(typeof(PostToolUseHookOutput))]
    [JsonSerializable(typeof(PostToolUseSpecificOutput))]
    [JsonSerializable(typeof(UserPromptSubmitHookOutput))]
    [JsonSerializable(typeof(UserPromptSubmitSpecificOutput))]
    [JsonSerializable(typeof(StopHookOutput))]
    [JsonSerializable(typeof(SubagentStopHookOutput))]
    [JsonSerializable(typeof(SessionStartHookOutput))]
    [JsonSerializable(typeof(SessionStartSpecificOutput))]
    [JsonSerializable(typeof(NotificationHookOutput))]
    [JsonSerializable(typeof(PreCompactHookOutput))]
    [JsonSerializable(typeof(SessionEndHookOutput))]

    // Tool input types
    [JsonSerializable(typeof(ReadToolInput))]
    [JsonSerializable(typeof(WriteToolInput))]
    [JsonSerializable(typeof(EditToolInput))]
    [JsonSerializable(typeof(BashToolInput))]
    [JsonSerializable(typeof(GlobToolInput))]
    [JsonSerializable(typeof(GrepToolInput))]
    [JsonSerializable(typeof(TaskToolInput))]
    [JsonSerializable(typeof(WebFetchToolInput))]
    [JsonSerializable(typeof(WebSearchToolInput))]
    [JsonSerializable(typeof(TodoWriteToolInput))]
    [JsonSerializable(typeof(TodoItem))]
    [JsonSerializable(typeof(TodoItem[]))]
    [JsonSerializable(typeof(NotebookEditToolInput))]
    [JsonSerializable(typeof(KillShellToolInput))]

    // Strongly-typed hook inputs with specific tool types
    [JsonSerializable(typeof(PreToolUseHookInput<ReadToolInput>), TypeInfoPropertyName = "PreToolUseHookInputRead")]
    [JsonSerializable(typeof(PreToolUseHookInput<WriteToolInput>), TypeInfoPropertyName = "PreToolUseHookInputWrite")]
    [JsonSerializable(typeof(PreToolUseHookInput<EditToolInput>), TypeInfoPropertyName = "PreToolUseHookInputEdit")]
    [JsonSerializable(typeof(PreToolUseHookInput<BashToolInput>), TypeInfoPropertyName = "PreToolUseHookInputBash")]
    [JsonSerializable(typeof(PreToolUseHookInput<GlobToolInput>), TypeInfoPropertyName = "PreToolUseHookInputGlob")]
    [JsonSerializable(typeof(PreToolUseHookInput<GrepToolInput>), TypeInfoPropertyName = "PreToolUseHookInputGrep")]
    [JsonSerializable(typeof(PreToolUseHookInput<TaskToolInput>), TypeInfoPropertyName = "PreToolUseHookInputTask")]
    [JsonSerializable(typeof(PreToolUseHookInput<WebFetchToolInput>), TypeInfoPropertyName = "PreToolUseHookInputWebFetch")]
    [JsonSerializable(typeof(PreToolUseHookInput<WebSearchToolInput>), TypeInfoPropertyName = "PreToolUseHookInputWebSearch")]
    [JsonSerializable(typeof(PreToolUseHookInput<TodoWriteToolInput>), TypeInfoPropertyName = "PreToolUseHookInputTodoWrite")]
    [JsonSerializable(typeof(PreToolUseHookInput<NotebookEditToolInput>), TypeInfoPropertyName = "PreToolUseHookInputNotebookEdit")]
    [JsonSerializable(typeof(PreToolUseHookInput<KillShellToolInput>), TypeInfoPropertyName = "PreToolUseHookInputKillShell")]
    public partial class ClaudeHooksJsonContext : JsonSerializerContext
    {

    }

}
