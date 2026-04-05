using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Outputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

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
    [JsonSerializable(typeof(WorktreeAction))]
    [JsonSerializable(typeof(TodoItemStatus))]
    [JsonSerializable(typeof(NotebookCellType))]
    [JsonSerializable(typeof(NotebookEditMode))]
    [JsonSerializable(typeof(GrepOutputMode))]
    [JsonSerializable(typeof(WriteOperationType))]
    [JsonSerializable(typeof(GitDiffStatus))]
    [JsonSerializable(typeof(TaskExecutionStatus))]
    [JsonSerializable(typeof(ConfigOperation))]
    [JsonSerializable(typeof(ContentBlockType))]
    [JsonSerializable(typeof(ClaudeModel))]

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
    [JsonSerializable(typeof(AgentToolInput))]
    [JsonSerializable(typeof(TaskOutputToolInput))]
    [JsonSerializable(typeof(ExitPlanModeToolInput))]
    [JsonSerializable(typeof(ExitPlanModeAllowedPrompt))]
    [JsonSerializable(typeof(ListMcpResourcesToolInput))]
    [JsonSerializable(typeof(McpToolInput))]
    [JsonSerializable(typeof(ReadMcpResourceToolInput))]
    [JsonSerializable(typeof(AskUserQuestionToolInput))]
    [JsonSerializable(typeof(AskUserQuestion))]
    [JsonSerializable(typeof(AskUserQuestionOption))]
    [JsonSerializable(typeof(ConfigToolInput))]
    [JsonSerializable(typeof(EnterWorktreeToolInput))]
    [JsonSerializable(typeof(ExitWorktreeToolInput))]
    [JsonSerializable(typeof(TaskStopToolInput))]

    // Tool response types
    [JsonSerializable(typeof(ReadToolResponse))]
    [JsonSerializable(typeof(ReadToolFileInfo))]
    [JsonSerializable(typeof(WriteToolResponse))]
    [JsonSerializable(typeof(EditToolResponse))]
    [JsonSerializable(typeof(StructuredPatchHunk))]
    [JsonSerializable(typeof(GitDiffInfo))]
    [JsonSerializable(typeof(BashToolResponse))]
    [JsonSerializable(typeof(GlobToolResponse))]
    [JsonSerializable(typeof(GrepToolResponse))]
    [JsonSerializable(typeof(TaskToolResponse))]
    [JsonSerializable(typeof(WebFetchToolResponse))]
    [JsonSerializable(typeof(WebSearchToolResponse))]
    [JsonSerializable(typeof(WebSearchResultContainer))]
    [JsonSerializable(typeof(WebSearchResultItem))]
    [JsonSerializable(typeof(TodoWriteToolResponse))]
    [JsonSerializable(typeof(NotebookEditToolResponse))]
    [JsonSerializable(typeof(KillShellToolResponse))]
    [JsonSerializable(typeof(TextReadToolResponse))]
    [JsonSerializable(typeof(ImageReadToolResponse))]
    [JsonSerializable(typeof(ImageReadToolFileInfo))]
    [JsonSerializable(typeof(ImageDimensionInfo))]
    [JsonSerializable(typeof(NotebookReadToolResponse))]
    [JsonSerializable(typeof(NotebookReadToolFileInfo))]
    [JsonSerializable(typeof(PdfReadToolResponse))]
    [JsonSerializable(typeof(PdfReadToolFileInfo))]
    [JsonSerializable(typeof(PartsReadToolResponse))]
    [JsonSerializable(typeof(PartsReadToolFileInfo))]
    [JsonSerializable(typeof(FileUnchangedReadToolResponse))]
    [JsonSerializable(typeof(FileUnchangedReadToolFileInfo))]
    [JsonSerializable(typeof(AgentToolResponse))]
    [JsonSerializable(typeof(CompletedAgentToolResponse))]
    [JsonSerializable(typeof(AsyncLaunchedAgentToolResponse))]
    [JsonSerializable(typeof(AgentContentBlock))]
    [JsonSerializable(typeof(AgentUsageInfo))]
    [JsonSerializable(typeof(AgentServerToolUseInfo))]
    [JsonSerializable(typeof(AgentCacheCreationInfo))]
    [JsonSerializable(typeof(ExitPlanModeToolResponse))]
    [JsonSerializable(typeof(List<McpResourceInfo>))]
    [JsonSerializable(typeof(McpResourceInfo))]
    [JsonSerializable(typeof(ReadMcpResourceToolResponse))]
    [JsonSerializable(typeof(McpResourceContent))]
    [JsonSerializable(typeof(AskUserQuestionToolResponse))]
    [JsonSerializable(typeof(AskUserQuestionAnnotation))]
    [JsonSerializable(typeof(ConfigToolResponse))]
    [JsonSerializable(typeof(EnterWorktreeToolResponse))]
    [JsonSerializable(typeof(ExitWorktreeToolResponse))]
    [JsonSerializable(typeof(TaskStopToolResponse))]

    // Strongly-typed PreToolUse output types for each tool
    [JsonSerializable(typeof(PreToolUseHookOutput<AgentToolInput>), TypeInfoPropertyName = "AgentPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<AskUserQuestionToolInput>), TypeInfoPropertyName = "AskUserQuestionPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<BashToolInput>), TypeInfoPropertyName = "BashPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<ConfigToolInput>), TypeInfoPropertyName = "ConfigPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<EditToolInput>), TypeInfoPropertyName = "EditPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<EnterWorktreeToolInput>), TypeInfoPropertyName = "EnterWorktreePreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<ExitPlanModeToolInput>), TypeInfoPropertyName = "ExitPlanModePreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<ExitWorktreeToolInput>), TypeInfoPropertyName = "ExitWorktreePreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<GlobToolInput>), TypeInfoPropertyName = "GlobPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<GrepToolInput>), TypeInfoPropertyName = "GrepPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<KillShellToolInput>), TypeInfoPropertyName = "KillShellPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<ListMcpResourcesToolInput>), TypeInfoPropertyName = "ListMcpResourcesPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<McpToolInput>), TypeInfoPropertyName = "McpPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<NotebookEditToolInput>), TypeInfoPropertyName = "NotebookEditPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<ReadToolInput>), TypeInfoPropertyName = "ReadPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<ReadMcpResourceToolInput>), TypeInfoPropertyName = "ReadMcpResourcePreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<TaskToolInput>), TypeInfoPropertyName = "TaskPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<TaskOutputToolInput>), TypeInfoPropertyName = "TaskOutputPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<TaskStopToolInput>), TypeInfoPropertyName = "TaskStopPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<TodoWriteToolInput>), TypeInfoPropertyName = "TodoWritePreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<WebFetchToolInput>), TypeInfoPropertyName = "WebFetchPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<WebSearchToolInput>), TypeInfoPropertyName = "WebSearchPreToolUseHookOutput")]
    [JsonSerializable(typeof(PreToolUseHookOutput<WriteToolInput>), TypeInfoPropertyName = "WritePreToolUseHookOutput")]

    // Strongly-typed PreToolUseSpecificOutput types for each tool
    [JsonSerializable(typeof(PreToolUseSpecificOutput<AgentToolInput>), TypeInfoPropertyName = "AgentPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<AskUserQuestionToolInput>), TypeInfoPropertyName = "AskUserQuestionPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<BashToolInput>), TypeInfoPropertyName = "BashPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<ConfigToolInput>), TypeInfoPropertyName = "ConfigPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<EditToolInput>), TypeInfoPropertyName = "EditPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<EnterWorktreeToolInput>), TypeInfoPropertyName = "EnterWorktreePreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<ExitPlanModeToolInput>), TypeInfoPropertyName = "ExitPlanModePreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<ExitWorktreeToolInput>), TypeInfoPropertyName = "ExitWorktreePreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<GlobToolInput>), TypeInfoPropertyName = "GlobPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<GrepToolInput>), TypeInfoPropertyName = "GrepPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<KillShellToolInput>), TypeInfoPropertyName = "KillShellPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<ListMcpResourcesToolInput>), TypeInfoPropertyName = "ListMcpResourcesPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<McpToolInput>), TypeInfoPropertyName = "McpPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<NotebookEditToolInput>), TypeInfoPropertyName = "NotebookEditPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<ReadToolInput>), TypeInfoPropertyName = "ReadPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<ReadMcpResourceToolInput>), TypeInfoPropertyName = "ReadMcpResourcePreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<TaskToolInput>), TypeInfoPropertyName = "TaskPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<TaskOutputToolInput>), TypeInfoPropertyName = "TaskOutputPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<TaskStopToolInput>), TypeInfoPropertyName = "TaskStopPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<TodoWriteToolInput>), TypeInfoPropertyName = "TodoWritePreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<WebFetchToolInput>), TypeInfoPropertyName = "WebFetchPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<WebSearchToolInput>), TypeInfoPropertyName = "WebSearchPreToolUseSpecificOutput")]
    [JsonSerializable(typeof(PreToolUseSpecificOutput<WriteToolInput>), TypeInfoPropertyName = "WritePreToolUseSpecificOutput")]

    // Strongly-typed PreToolUse payloads for each tool
    [JsonSerializable(typeof(ReadPreToolUsePayload))]
    [JsonSerializable(typeof(WritePreToolUsePayload))]
    [JsonSerializable(typeof(EditPreToolUsePayload))]
    [JsonSerializable(typeof(BashPreToolUsePayload))]
    [JsonSerializable(typeof(GlobPreToolUsePayload))]
    [JsonSerializable(typeof(GrepPreToolUsePayload))]
    [JsonSerializable(typeof(TaskPreToolUsePayload))]
    [JsonSerializable(typeof(WebFetchPreToolUsePayload))]
    [JsonSerializable(typeof(WebSearchPreToolUsePayload))]
    [JsonSerializable(typeof(TodoWritePreToolUsePayload))]
    [JsonSerializable(typeof(NotebookEditPreToolUsePayload))]
    [JsonSerializable(typeof(KillShellPreToolUsePayload))]
    [JsonSerializable(typeof(AgentPreToolUsePayload))]
    [JsonSerializable(typeof(TaskOutputPreToolUsePayload))]
    [JsonSerializable(typeof(ExitPlanModePreToolUsePayload))]
    [JsonSerializable(typeof(ListMcpResourcesPreToolUsePayload))]
    [JsonSerializable(typeof(McpPreToolUsePayload))]
    [JsonSerializable(typeof(ReadMcpResourcePreToolUsePayload))]
    [JsonSerializable(typeof(AskUserQuestionPreToolUsePayload))]
    [JsonSerializable(typeof(ConfigPreToolUsePayload))]
    [JsonSerializable(typeof(EnterWorktreePreToolUsePayload))]
    [JsonSerializable(typeof(ExitWorktreePreToolUsePayload))]
    [JsonSerializable(typeof(TaskStopPreToolUsePayload))]

    // Strongly-typed PostToolUse payloads for each tool
    [JsonSerializable(typeof(ReadPostToolUsePayload))]
    [JsonSerializable(typeof(WritePostToolUsePayload))]
    [JsonSerializable(typeof(EditPostToolUsePayload))]
    [JsonSerializable(typeof(BashPostToolUsePayload))]
    [JsonSerializable(typeof(GlobPostToolUsePayload))]
    [JsonSerializable(typeof(GrepPostToolUsePayload))]
    [JsonSerializable(typeof(TaskPostToolUsePayload))]
    [JsonSerializable(typeof(WebFetchPostToolUsePayload))]
    [JsonSerializable(typeof(WebSearchPostToolUsePayload))]
    [JsonSerializable(typeof(TodoWritePostToolUsePayload))]
    [JsonSerializable(typeof(NotebookEditPostToolUsePayload))]
    [JsonSerializable(typeof(KillShellPostToolUsePayload))]
    [JsonSerializable(typeof(AgentPostToolUsePayload))]
    [JsonSerializable(typeof(TaskOutputPostToolUsePayload))]
    [JsonSerializable(typeof(ExitPlanModePostToolUsePayload))]
    [JsonSerializable(typeof(ListMcpResourcesPostToolUsePayload))]
    [JsonSerializable(typeof(McpPostToolUsePayload))]
    [JsonSerializable(typeof(ReadMcpResourcePostToolUsePayload))]
    [JsonSerializable(typeof(AskUserQuestionPostToolUsePayload))]
    [JsonSerializable(typeof(ConfigPostToolUsePayload))]
    [JsonSerializable(typeof(EnterWorktreePostToolUsePayload))]
    [JsonSerializable(typeof(ExitWorktreePostToolUsePayload))]
    [JsonSerializable(typeof(TaskStopPostToolUsePayload))]
    public partial class ClaudeHooksJsonContext : JsonSerializerContext
    {

    }

}
