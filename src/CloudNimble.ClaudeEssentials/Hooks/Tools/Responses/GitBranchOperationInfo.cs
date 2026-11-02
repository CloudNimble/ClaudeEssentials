using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a branch integration detected in a Bash command.
    /// </summary>
    public class GitBranchOperationInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the operation that was performed.
        /// </summary>
        [JsonPropertyName("action")]
        public GitBranchAction Action { get; set; }

        /// <summary>
        /// Gets or sets the ref involved in the operation.
        /// </summary>
        [JsonPropertyName("ref")]
        public string Ref { get; set; } = string.Empty;

        #endregion

    }

}
