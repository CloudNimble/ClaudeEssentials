using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the source that triggered a session start event.
    /// </summary>
    public enum SessionStartSource
    {

        /// <summary>
        /// Session started from initial startup of Claude Code.
        /// </summary>
        [JsonStringEnumMemberName("startup")]
        Startup,

        /// <summary>
        /// Session resumed from a previous session.
        /// </summary>
        [JsonStringEnumMemberName("resume")]
        Resume,

        /// <summary>
        /// Session started after a clear command.
        /// </summary>
        [JsonStringEnumMemberName("clear")]
        Clear,

        /// <summary>
        /// Session started after a compact operation.
        /// </summary>
        [JsonStringEnumMemberName("compact")]
        Compact

    }

}
