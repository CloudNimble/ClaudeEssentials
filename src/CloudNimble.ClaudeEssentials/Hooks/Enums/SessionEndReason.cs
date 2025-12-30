namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the reason why a session ended.
    /// </summary>
    public enum SessionEndReason
    {

        /// <summary>
        /// Session ended due to a clear command.
        /// </summary>
        Clear,

        /// <summary>
        /// Session ended due to user logout.
        /// </summary>
        Logout,

        /// <summary>
        /// Session ended due to user exiting from prompt input.
        /// </summary>
        PromptInputExit,

        /// <summary>
        /// Session ended for another unspecified reason.
        /// </summary>
        Other

    }

}
