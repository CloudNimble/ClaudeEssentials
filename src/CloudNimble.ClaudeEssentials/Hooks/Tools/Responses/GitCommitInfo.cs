using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a commit detected in a Bash command.
    /// </summary>
    public class GitCommitInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the branch the commit is on.
        /// </summary>
        [JsonPropertyName("branch")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Branch { get; set; }

        /// <summary>
        /// Gets or sets how the commit was produced.
        /// </summary>
        [JsonPropertyName("kind")]
        public GitCommitKind Kind { get; set; }

        /// <summary>
        /// Gets or sets the commit SHA.
        /// </summary>
        [JsonPropertyName("sha")]
        public string Sha { get; set; } = string.Empty;

        #endregion

    }

}
