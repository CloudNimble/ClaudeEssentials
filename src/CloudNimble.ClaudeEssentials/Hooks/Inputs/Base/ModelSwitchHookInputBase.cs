using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Base class for the inputs received by hooks that run around a model switch.
    /// </summary>
    public abstract class ModelSwitchHookInputBase : HookInputBase
    {

        #region Properties

        /// <summary>
        /// Gets or sets the prompt cache time to live.
        /// </summary>
        [JsonPropertyName("cache_ttl")]
        public PromptCacheTtl CacheTtl { get; set; }

        /// <summary>
        /// Gets or sets the prompt tokens the next request re-sends.
        /// </summary>
        [JsonPropertyName("context_tokens")]
        public long ContextTokens { get; set; }

        /// <summary>
        /// Gets or sets the estimated cost of re-caching the context on the target model, excluding the response.
        /// </summary>
        [JsonPropertyName("estimated_cache_write_usd")]
        public double EstimatedCacheWriteUsd { get; set; }

        /// <summary>
        /// Gets or sets the resolved model ID the session was running before the switch.
        /// </summary>
        [JsonPropertyName("from_model")]
        public string FromModel { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets where the cost estimate came from.
        /// </summary>
        [JsonPropertyName("pricing")]
        public ModelSwitchPricing Pricing { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current model's prompt cache is likely still warm.
        /// </summary>
        [JsonPropertyName("prompt_cache_warm")]
        public bool PromptCacheWarm { get; set; }

        /// <summary>
        /// Gets or sets what was asked for, such as an alias or full ID, or null for the default.
        /// </summary>
        [JsonPropertyName("requested_model")]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string? RequestedModel { get; set; }

        /// <summary>
        /// Gets or sets what initiated the switch.
        /// </summary>
        [JsonPropertyName("source")]
        public ModelSwitchSource Source { get; set; }

        /// <summary>
        /// Gets or sets the resolved model ID the session runs after the switch.
        /// </summary>
        [JsonPropertyName("to_model")]
        public string ToModel { get; set; } = string.Empty;

        #endregion

    }

}
