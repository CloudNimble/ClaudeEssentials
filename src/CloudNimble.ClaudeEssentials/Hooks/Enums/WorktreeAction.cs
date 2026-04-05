using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the action to take when exiting a git worktree.
    /// </summary>
    /// <remarks>
    /// Claude Code uses lowercase values for worktree actions in the wire format.
    /// </remarks>
    public enum WorktreeAction
    {

        /// <summary>
        /// Leaves the worktree and branch on disk.
        /// </summary>
        [JsonStringEnumMemberName("keep")]
        Keep,

        /// <summary>
        /// Deletes both the worktree and branch.
        /// </summary>
        [JsonStringEnumMemberName("remove")]
        Remove

    }

}
