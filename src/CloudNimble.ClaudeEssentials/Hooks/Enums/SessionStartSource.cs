namespace CloudNimble.ClaudeEssentials.Hooks.Enums
{

    /// <summary>
    /// Represents the source that triggered a session start event.
    /// </summary>
    public enum SessionStartSource
    {

        /// <summary>
        /// Session started from initial startup of Claude Code.
        /// </summary>
        Startup,

        /// <summary>
        /// Session resumed from a previous session.
        /// </summary>
        Resume,

        /// <summary>
        /// Session started after a clear command.
        /// </summary>
        Clear,

        /// <summary>
        /// Session started after a compact operation.
        /// </summary>
        Compact

    }

}
