namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the type of notification sent by Claude Code.
    /// </summary>
    public enum NotificationType
    {

        /// <summary>
        /// A permission prompt notification requiring user action.
        /// </summary>
        PermissionPrompt,

        /// <summary>
        /// An idle prompt notification indicating Claude is waiting for input.
        /// </summary>
        IdlePrompt,

        /// <summary>
        /// A notification indicating successful authentication.
        /// </summary>
        AuthSuccess,

        /// <summary>
        /// An elicitation dialog notification for gathering user input.
        /// </summary>
        ElicitationDialog

    }

}
