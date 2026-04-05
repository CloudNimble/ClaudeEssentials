using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the Claude Code ExitWorktree tool
    /// after exiting and optionally removing a git worktree.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "action": "remove",
    ///   "originalCwd": "/home/user/project",
    ///   "worktreePath": "/tmp/worktrees/feature-abc",
    ///   "worktreeBranch": "worktree/feature-abc",
    ///   "discardedFiles": 3,
    ///   "discardedCommits": 1,
    ///   "message": "Removed worktree and returned to /home/user/project"
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class ExitWorktreeToolResponse
    {

        /// <summary>
        /// Gets or sets the action that was performed.
        /// </summary>
        [JsonPropertyName("action")]
        public WorktreeAction Action { get; set; }

        /// <summary>
        /// Gets or sets the original working directory before the worktree was entered.
        /// </summary>
        [JsonPropertyName("originalCwd")]
        public string OriginalCwd { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file system path of the worktree that was exited.
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
        /// Gets or sets the tmux session name, if applicable.
        /// </summary>
        [JsonPropertyName("tmuxSessionName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TmuxSessionName { get; set; }

        /// <summary>
        /// Gets or sets the number of uncommitted files that were discarded when removing the worktree.
        /// </summary>
        [JsonPropertyName("discardedFiles")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? DiscardedFiles { get; set; }

        /// <summary>
        /// Gets or sets the number of unmerged commits that were discarded when removing the worktree.
        /// </summary>
        [JsonPropertyName("discardedCommits")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? DiscardedCommits { get; set; }

        /// <summary>
        /// Gets or sets a human-readable message about the operation.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

    }

}
