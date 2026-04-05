using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the different types of hook events that can be triggered in Claude Code.
    /// </summary>
    /// <remarks>
    /// Hook event names use PascalCase on the wire (e.g., <c>"PreToolUse"</c>, <c>"SessionStart"</c>).
    /// </remarks>
    public enum HookEventName
    {

        /// <summary>
        /// Runs when Claude Code starts a new session or resumes one.
        /// </summary>
        [JsonStringEnumMemberName("SessionStart")]
        SessionStart,

        /// <summary>
        /// Runs when instructions (CLAUDE.md files, etc.) are loaded.
        /// </summary>
        [JsonStringEnumMemberName("InstructionsLoaded")]
        InstructionsLoaded,

        /// <summary>
        /// Runs when the user submits a prompt, before Claude processes it.
        /// </summary>
        [JsonStringEnumMemberName("UserPromptSubmit")]
        UserPromptSubmit,

        /// <summary>
        /// Runs before tool calls are executed. Can be used to block or modify tool inputs.
        /// </summary>
        [JsonStringEnumMemberName("PreToolUse")]
        PreToolUse,

        /// <summary>
        /// Runs when a permission dialog is shown. Can be used to automatically allow or deny permissions.
        /// </summary>
        [JsonStringEnumMemberName("PermissionRequest")]
        PermissionRequest,

        /// <summary>
        /// Runs when a permission request is denied.
        /// </summary>
        [JsonStringEnumMemberName("PermissionDenied")]
        PermissionDenied,

        /// <summary>
        /// Runs after tool calls complete. Can be used to inspect results or provide additional context.
        /// </summary>
        [JsonStringEnumMemberName("PostToolUse")]
        PostToolUse,

        /// <summary>
        /// Runs after a tool call fails.
        /// </summary>
        [JsonStringEnumMemberName("PostToolUseFailure")]
        PostToolUseFailure,

        /// <summary>
        /// Runs when Claude Code sends notifications.
        /// </summary>
        [JsonStringEnumMemberName("Notification")]
        Notification,

        /// <summary>
        /// Runs when a subagent task starts.
        /// </summary>
        [JsonStringEnumMemberName("SubagentStart")]
        SubagentStart,

        /// <summary>
        /// Runs when subagent tasks complete.
        /// </summary>
        [JsonStringEnumMemberName("SubagentStop")]
        SubagentStop,

        /// <summary>
        /// Runs when a task is created.
        /// </summary>
        [JsonStringEnumMemberName("TaskCreated")]
        TaskCreated,

        /// <summary>
        /// Runs when a task completes.
        /// </summary>
        [JsonStringEnumMemberName("TaskCompleted")]
        TaskCompleted,

        /// <summary>
        /// Runs when Claude Code finishes responding.
        /// </summary>
        [JsonStringEnumMemberName("Stop")]
        Stop,

        /// <summary>
        /// Runs when a stop hook itself fails.
        /// </summary>
        [JsonStringEnumMemberName("StopFailure")]
        StopFailure,

        /// <summary>
        /// Runs when a teammate agent becomes idle.
        /// </summary>
        [JsonStringEnumMemberName("TeammateIdle")]
        TeammateIdle,

        /// <summary>
        /// Runs when the current working directory changes.
        /// </summary>
        [JsonStringEnumMemberName("CwdChanged")]
        CwdChanged,

        /// <summary>
        /// Runs when a file is changed externally.
        /// </summary>
        [JsonStringEnumMemberName("FileChanged")]
        FileChanged,

        /// <summary>
        /// Runs when a git worktree is created.
        /// </summary>
        [JsonStringEnumMemberName("WorktreeCreate")]
        WorktreeCreate,

        /// <summary>
        /// Runs when a git worktree is removed.
        /// </summary>
        [JsonStringEnumMemberName("WorktreeRemove")]
        WorktreeRemove,

        /// <summary>
        /// Runs before a compact operation.
        /// </summary>
        [JsonStringEnumMemberName("PreCompact")]
        PreCompact,

        /// <summary>
        /// Runs after a compact operation completes.
        /// </summary>
        [JsonStringEnumMemberName("PostCompact")]
        PostCompact,

        /// <summary>
        /// Runs when an elicitation dialog is presented to the user.
        /// </summary>
        [JsonStringEnumMemberName("Elicitation")]
        Elicitation,

        /// <summary>
        /// Runs when the user responds to an elicitation dialog.
        /// </summary>
        [JsonStringEnumMemberName("ElicitationResult")]
        ElicitationResult,

        /// <summary>
        /// Runs when a Claude Code session ends.
        /// </summary>
        [JsonStringEnumMemberName("SessionEnd")]
        SessionEnd,

        /// <summary>
        /// Runs when a configuration setting changes.
        /// </summary>
        [JsonStringEnumMemberName("ConfigChange")]
        ConfigChange

    }

}
