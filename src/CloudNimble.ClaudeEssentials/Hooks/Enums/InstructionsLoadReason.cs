using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the reason why an instruction file was loaded in Claude Code.
    /// </summary>
    public enum InstructionsLoadReason
    {

        /// <summary>
        /// The instruction file was loaded during session start.
        /// </summary>
        [JsonStringEnumMemberName("session_start")]
        SessionStart,

        /// <summary>
        /// The instruction file was loaded by traversing nested directories.
        /// </summary>
        [JsonStringEnumMemberName("nested_traversal")]
        NestedTraversal,

        /// <summary>
        /// The instruction file was loaded because a path glob pattern matched.
        /// </summary>
        [JsonStringEnumMemberName("path_glob_match")]
        PathGlobMatch,

        /// <summary>
        /// The instruction file was loaded because it was included by another instruction file.
        /// </summary>
        [JsonStringEnumMemberName("include")]
        Include,

        /// <summary>
        /// The instruction file was reloaded after a compact operation.
        /// </summary>
        [JsonStringEnumMemberName("compact")]
        Compact

    }

}
