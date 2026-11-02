using System.Text.Json.Serialization;
using System.Text.Json;

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
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public int? CacheCreationInputTokens { get; set; }

        /// <summary>
        /// Gets or sets the number of input tokens read from cache.
        /// </summary>
        [JsonPropertyName("cache_read_input_tokens")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public int? CacheReadInputTokens { get; set; }

        /// <summary>
        /// Gets or sets the server-side tool use counts.
        /// </summary>
        [JsonPropertyName("server_tool_use")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public AgentServerToolUseInfo? ServerToolUse { get; set; }

        /// <summary>
        /// Gets or sets the service tier used for this execution.
        /// </summary>
        /// <remarks>
        /// Known values: <c>"standard"</c>, <c>"priority"</c>, <c>"batch"</c>.
        /// </remarks>
        [JsonPropertyName("service_tier")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string? ServiceTier { get; set; }

        /// <summary>
        /// Gets or sets the cache creation breakdown by ephemeral duration.
        /// </summary>
        [JsonPropertyName("cache_creation")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public AgentCacheCreationInfo? CacheCreation { get; set; }

        /// <summary>
        /// Gets or sets the geography where inference ran.
        /// </summary>
        [JsonPropertyName("inference_geo")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? InferenceGeo { get; set; }

        /// <summary>
        /// Gets or sets the inference speed tier.
        /// </summary>
        [JsonPropertyName("speed")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Speed { get; set; }

        /// <summary>
        /// Gets or sets the per-iteration usage details.
        /// </summary>
        [JsonPropertyName("iterations")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? Iterations { get; set; }

        /// <summary>
        /// Gets or sets a breakdown of the output tokens.
        /// </summary>
        [JsonPropertyName("output_tokens_details")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AgentOutputTokensDetails? OutputTokensDetails { get; set; }

        /// <summary>
        /// Gets or sets the fallback credit details.
        /// </summary>
        [JsonPropertyName("fallback_credit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? FallbackCredit { get; set; }

    }

}
