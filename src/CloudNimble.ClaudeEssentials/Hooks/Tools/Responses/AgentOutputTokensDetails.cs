using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a breakdown of the output tokens used by an agent.
    /// </summary>
    public class AgentOutputTokensDetails
    {

        #region Properties

        /// <summary>
        /// Gets or sets the number of tokens used for thinking.
        /// </summary>
        [JsonPropertyName("thinking_tokens")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ThinkingTokens { get; set; }

        #endregion

    }

}
