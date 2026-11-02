using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the git and GitHub operations detected in a Bash command.
    /// </summary>
    /// <remarks>
    /// This lets clients render git activity without re-parsing stdout, and it is not surfaced to the model.
    /// </remarks>
    public class GitOperationInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the branch integration that was performed.
        /// </summary>
        [JsonPropertyName("branch")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public GitBranchOperationInfo? Branch { get; set; }

        /// <summary>
        /// Gets or sets the commit that was created.
        /// </summary>
        [JsonPropertyName("commit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public GitCommitInfo? Commit { get; set; }

        /// <summary>
        /// Gets or sets the pull request operation that was performed.
        /// </summary>
        [JsonPropertyName("pr")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public GitPullRequestInfo? PullRequest { get; set; }

        /// <summary>
        /// Gets or sets the push that was performed.
        /// </summary>
        [JsonPropertyName("push")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public GitPushInfo? Push { get; set; }

        #endregion

    }

}
