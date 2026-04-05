using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the Claude Code EnterWorktree tool
    /// after creating a temporary git worktree.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "worktreePath": "/tmp/worktrees/feature-abc",
    ///   "worktreeBranch": "worktree/feature-abc",
    ///   "message": "Created worktree at /tmp/worktrees/feature-abc"
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class EnterWorktreeToolResponse
    {

        /// <summary>
        /// Gets or sets the file system path to the created worktree.
        /// </summary>
        [JsonPropertyName("worktreePath")]
        public string WorktreePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the git branch name associated with the worktree.
        /// </summary>
        [JsonPropertyName("worktreeBranch")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? WorktreeBranch { get; set; }

        /// <summary>
        /// Gets or sets a human-readable message about the operation.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

    }

}
