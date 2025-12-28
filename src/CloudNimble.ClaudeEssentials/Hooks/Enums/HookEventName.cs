namespace CloudNimble.ClaudeEssentials.Hooks.Enums
{

    /// <summary>
    /// Represents the different types of hook events that can be triggered in Claude Code.
    /// </summary>
    public enum HookEventName
    {

        /// <summary>
        /// Runs before tool calls are executed. Can be used to block or modify tool inputs.
        /// </summary>
        PreToolUse,

        /// <summary>
        /// Runs when a permission dialog is shown. Can be used to automatically allow or deny permissions.
        /// </summary>
        PermissionRequest,

        /// <summary>
        /// Runs after tool calls complete. Can be used to inspect results or provide additional context.
        /// </summary>
        PostToolUse,

        /// <summary>
        /// Runs when the user submits a prompt, before Claude processes it.
        /// </summary>
        UserPromptSubmit,

        /// <summary>
        /// Runs when Claude Code sends notifications.
        /// </summary>
        Notification,

        /// <summary>
        /// Runs when Claude Code finishes responding.
        /// </summary>
        Stop,

        /// <summary>
        /// Runs when subagent tasks complete.
        /// </summary>
        SubagentStop,

        /// <summary>
        /// Runs before a compact operation.
        /// </summary>
        PreCompact,

        /// <summary>
        /// Runs when Claude Code starts a new session or resumes one.
        /// </summary>
        SessionStart,

        /// <summary>
        /// Runs when a Claude Code session ends.
        /// </summary>
        SessionEnd

    }

}
