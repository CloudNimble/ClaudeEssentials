using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a push detected in a Bash command.
    /// </summary>
    public class GitPushInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the branch that was pushed.
        /// </summary>
        [JsonPropertyName("branch")]
        public string Branch { get; set; } = string.Empty;

        #endregion

    }

}
