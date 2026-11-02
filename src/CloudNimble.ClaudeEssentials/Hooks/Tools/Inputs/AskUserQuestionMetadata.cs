using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents optional tracking metadata for an AskUserQuestion invocation, which is not displayed to the user.
    /// </summary>
    public class AskUserQuestionMetadata
    {

        #region Properties

        /// <summary>
        /// Gets or sets the optional identifier for the source of this question, used for analytics tracking.
        /// </summary>
        [JsonPropertyName("source")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Source { get; set; }

        #endregion

    }

}
