using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the hook event names that Claude Code can trigger.
    /// </summary>
    public enum HookEventName
    {

        /// <summary>
        /// Runs before a tool executes.
        /// </summary>
        [JsonStringEnumMemberName("PreToolUse")]
        PreToolUse,

        /// <summary>
        /// Runs after a tool completes successfully.
        /// </summary>
        [JsonStringEnumMemberName("PostToolUse")]
        PostToolUse,

        /// <summary>
        /// Runs after a tool fails.
        /// </summary>
        [JsonStringEnumMemberName("PostToolUseFailure")]
        PostToolUseFailure,

        /// <summary>
        /// Runs once after every tool call in a batch has resolved, before the next model request.
        /// </summary>
        [JsonStringEnumMemberName("PostToolBatch")]
        PostToolBatch,

        /// <summary>
        /// Runs when Claude Code sends a notification.
        /// </summary>
        [JsonStringEnumMemberName("Notification")]
        Notification,

        /// <summary>
        /// Runs when the user submits a prompt.
        /// </summary>
        [JsonStringEnumMemberName("UserPromptSubmit")]
        UserPromptSubmit,

        /// <summary>
        /// Runs when a slash command or MCP prompt expands into a prompt.
        /// </summary>
        [JsonStringEnumMemberName("UserPromptExpansion")]
        UserPromptExpansion,

        /// <summary>
        /// Runs when a session starts or resumes.
        /// </summary>
        [JsonStringEnumMemberName("SessionStart")]
        SessionStart,

        /// <summary>
        /// Runs when a session ends.
        /// </summary>
        [JsonStringEnumMemberName("SessionEnd")]
        SessionEnd,

        /// <summary>
        /// Runs when the main agent finishes responding.
        /// </summary>
        [JsonStringEnumMemberName("Stop")]
        Stop,

        /// <summary>
        /// Runs when a turn ends because of an API error.
        /// </summary>
        [JsonStringEnumMemberName("StopFailure")]
        StopFailure,

        /// <summary>
        /// Runs when a subagent starts.
        /// </summary>
        [JsonStringEnumMemberName("SubagentStart")]
        SubagentStart,

        /// <summary>
        /// Runs when a subagent finishes.
        /// </summary>
        [JsonStringEnumMemberName("SubagentStop")]
        SubagentStop,

        /// <summary>
        /// Runs before context compaction.
        /// </summary>
        [JsonStringEnumMemberName("PreCompact")]
        PreCompact,

        /// <summary>
        /// Runs after context compaction.
        /// </summary>
        [JsonStringEnumMemberName("PostCompact")]
        PostCompact,

        /// <summary>
        /// Runs before the session switches models.
        /// </summary>
        [JsonStringEnumMemberName("PreModelSwitch")]
        PreModelSwitch,

        /// <summary>
        /// Runs after the session switches models.
        /// </summary>
        [JsonStringEnumMemberName("PostModelSwitch")]
        PostModelSwitch,

        /// <summary>
        /// Runs when a permission dialog would be shown.
        /// </summary>
        [JsonStringEnumMemberName("PermissionRequest")]
        PermissionRequest,

        /// <summary>
        /// Runs when a tool call is denied.
        /// </summary>
        [JsonStringEnumMemberName("PermissionDenied")]
        PermissionDenied,

        /// <summary>
        /// Runs during repository setup or maintenance.
        /// </summary>
        [JsonStringEnumMemberName("Setup")]
        Setup,

        /// <summary>
        /// Runs when a teammate is about to go idle.
        /// </summary>
        [JsonStringEnumMemberName("TeammateIdle")]
        TeammateIdle,

        /// <summary>
        /// Runs when a task is created.
        /// </summary>
        [JsonStringEnumMemberName("TaskCreated")]
        TaskCreated,

        /// <summary>
        /// Runs when a task is completed.
        /// </summary>
        [JsonStringEnumMemberName("TaskCompleted")]
        TaskCompleted,

        /// <summary>
        /// Runs when an MCP server requests user input.
        /// </summary>
        [JsonStringEnumMemberName("Elicitation")]
        Elicitation,

        /// <summary>
        /// Runs after the user responds to an MCP elicitation, before the response is sent.
        /// </summary>
        [JsonStringEnumMemberName("ElicitationResult")]
        ElicitationResult,

        /// <summary>
        /// Runs when a configuration file changes during a session.
        /// </summary>
        [JsonStringEnumMemberName("ConfigChange")]
        ConfigChange,

        /// <summary>
        /// Runs when a worktree is created.
        /// </summary>
        [JsonStringEnumMemberName("WorktreeCreate")]
        WorktreeCreate,

        /// <summary>
        /// Runs when a worktree is removed.
        /// </summary>
        [JsonStringEnumMemberName("WorktreeRemove")]
        WorktreeRemove,

        /// <summary>
        /// Runs when a CLAUDE.md or rules file is loaded into context.
        /// </summary>
        [JsonStringEnumMemberName("InstructionsLoaded")]
        InstructionsLoaded,

        /// <summary>
        /// Runs when the working directory changes.
        /// </summary>
        [JsonStringEnumMemberName("CwdChanged")]
        CwdChanged,

        /// <summary>
        /// Runs when a watched file changes.
        /// </summary>
        [JsonStringEnumMemberName("FileChanged")]
        FileChanged,

        /// <summary>
        /// Runs when a directory is added to the session.
        /// </summary>
        [JsonStringEnumMemberName("DirectoryAdded")]
        DirectoryAdded,

        /// <summary>
        /// Runs as assistant message text is displayed.
        /// </summary>
        [JsonStringEnumMemberName("MessageDisplay")]
        MessageDisplay

    }

}
