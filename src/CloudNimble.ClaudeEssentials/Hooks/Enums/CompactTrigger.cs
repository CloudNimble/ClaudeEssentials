namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents what triggered a compact operation.
    /// </summary>
    public enum CompactTrigger
    {

        /// <summary>
        /// Compact was triggered manually by the user.
        /// </summary>
        Manual,

        /// <summary>
        /// Compact was triggered automatically by the system.
        /// </summary>
        Auto

    }

}
