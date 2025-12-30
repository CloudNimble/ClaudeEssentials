namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the permission mode under which Claude Code is operating.
    /// </summary>
    public enum PermissionMode
    {

        /// <summary>
        /// Default permission mode requiring explicit user approval for sensitive operations.
        /// </summary>
        Default,

        /// <summary>
        /// Plan mode where Claude explores and plans but doesn't execute changes.
        /// </summary>
        Plan,

        /// <summary>
        /// Mode that automatically accepts file edits without prompting.
        /// </summary>
        AcceptEdits,

        /// <summary>
        /// Mode that bypasses all permission prompts. Use with caution.
        /// </summary>
        BypassPermissions

    }

}
