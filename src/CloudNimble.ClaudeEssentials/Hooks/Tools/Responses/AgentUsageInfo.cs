using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Contains token usage information for a completed agent execution, including input/output
    /// token counts, cache statistics, and server-side tool use.
    /// </summary>
    public class AgentUsageInfo
    {

        /// <summary>
        /// Gets or sets the number of input tokens consumed.
        /// </summary>
        [JsonPropertyName("input_tokens")]
        public int InputTokens { get; set; }

        /// <summary>
        /// Gets or sets the number of output tokens generated.
        /// </summary>
        [JsonPropertyName("output_tokens")]
        public int OutputTokens { get; set; }

        /// <summary>
        /// Gets or sets the number of input tokens used for cache creation.
        /// </summary>
        [JsonPropertyName("cache_creation_input_tokens")]
        public int? CacheCreationInputTokens { get; set; }

        /// <summary>
        /// Gets or sets the number of input tokens read from cache.
        /// </summary>
        [JsonPropertyName("cache_read_input_tokens")]
        public int? CacheReadInputTokens { get; set; }

        /// <summary>
        /// Gets or sets the server-side tool use counts.
        /// </summary>
        [JsonPropertyName("server_tool_use")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AgentServerToolUseInfo? ServerToolUse { get; set; }

        /// <summary>
        /// Gets or sets the service tier used for this execution.
        /// </summary>
        /// <remarks>
        /// Known values: <c>"standard"</c>, <c>"priority"</c>, <c>"batch"</c>.
        /// </remarks>
        [JsonPropertyName("service_tier")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ServiceTier { get; set; }

        /// <summary>
        /// Gets or sets the cache creation breakdown by ephemeral duration.
        /// </summary>
        [JsonPropertyName("cache_creation")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AgentCacheCreationInfo? CacheCreation { get; set; }

    }

}
