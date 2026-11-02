using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents how a commit detected by the Bash tool was produced.
    /// </summary>
    public enum GitCommitKind
    {

        /// <summary>
        /// A new commit was created.
        /// </summary>
        [JsonStringEnumMemberName("committed")]
        Committed,

        /// <summary>
        /// An existing commit was amended.
        /// </summary>
        [JsonStringEnumMemberName("amended")]
        Amended,

        /// <summary>
        /// A commit was cherry-picked.
        /// </summary>
        [JsonStringEnumMemberName("cherry-picked")]
        CherryPicked

    }

}
