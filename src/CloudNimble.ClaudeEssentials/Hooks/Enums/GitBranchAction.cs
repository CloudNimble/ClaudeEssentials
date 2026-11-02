using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents a branch-integration operation detected by the Bash tool.
    /// </summary>
    public enum GitBranchAction
    {

        /// <summary>
        /// The branch was merged.
        /// </summary>
        [JsonStringEnumMemberName("merged")]
        Merged,

        /// <summary>
        /// The branch was rebased.
        /// </summary>
        [JsonStringEnumMemberName("rebased")]
        Rebased

    }

}
