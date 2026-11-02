using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a pull request operation detected in a Bash command.
    /// </summary>
    public class GitPullRequestInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the operation that was performed.
        /// </summary>
        [JsonPropertyName("action")]
        public GitPullRequestAction Action { get; set; }

        /// <summary>
        /// Gets or sets the pull request number.
        /// </summary>
        [JsonPropertyName("number")]
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the pull request URL.
        /// </summary>
        [JsonPropertyName("url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Url { get; set; }

        #endregion

    }

}
