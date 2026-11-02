using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents how a session started.
    /// </summary>
    public enum SessionStartSource
    {

        /// <summary>
        /// A new session started.
        /// </summary>
        [JsonStringEnumMemberName("startup")]
        Startup,

        /// <summary>
        /// An existing session was resumed.
        /// </summary>
        [JsonStringEnumMemberName("resume")]
        Resume,

        /// <summary>
        /// The session started after being cleared.
        /// </summary>
        [JsonStringEnumMemberName("clear")]
        Clear,

        /// <summary>
        /// The session started after compaction.
        /// </summary>
        [JsonStringEnumMemberName("compact")]
        Compact,

        /// <summary>
        /// The session was forked from another session.
        /// </summary>
        [JsonStringEnumMemberName("fork")]
        Fork

    }

}
