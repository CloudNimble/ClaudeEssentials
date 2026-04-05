using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Contains cache creation token counts for an agent execution, broken down by
    /// ephemeral cache duration.
    /// </summary>
    public class AgentCacheCreationInfo
    {

        /// <summary>
        /// Gets or sets the number of ephemeral 1-hour input tokens used for cache creation.
        /// </summary>
        [JsonPropertyName("ephemeral_1h_input_tokens")]
        public int Ephemeral1hInputTokens { get; set; }

        /// <summary>
        /// Gets or sets the number of ephemeral 5-minute input tokens used for cache creation.
        /// </summary>
        [JsonPropertyName("ephemeral_5m_input_tokens")]
        public int Ephemeral5mInputTokens { get; set; }

    }

}
