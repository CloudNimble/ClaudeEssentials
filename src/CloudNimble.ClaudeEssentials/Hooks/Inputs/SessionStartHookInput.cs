using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a SessionStart hook.
    /// This hook runs when Claude Code starts a new session or resumes one.
    /// </summary>
    public class SessionStartHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the source that triggered the session start.
        /// </summary>
        [JsonPropertyName("source")]
        public SessionStartSource Source { get; set; }

        /// <summary>
        /// Gets or sets the session model.
        /// </summary>
        [JsonPropertyName("model")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Model { get; set; }

        /// <summary>
        /// Gets or sets the session title.
        /// </summary>
        [JsonPropertyName("session_title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SessionTitle { get; set; }

        /// <summary>
        /// Gets or sets the seconds since the resumed transcript's last assistant response. Applies to resume and fork.
        /// </summary>
        [JsonPropertyName("seconds_since_last_response")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? SecondsSinceLastResponse { get; set; }

        /// <summary>
        /// Gets or sets the tokens in the resumed transcript's last response. Applies to resume and fork.
        /// </summary>
        [JsonPropertyName("context_tokens")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? ContextTokens { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the first request will re-cache the context because the prompt cache likely expired. Applies to resume and fork.
        /// </summary>
        [JsonPropertyName("prompt_cache_likely_expired")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? PromptCacheLikelyExpired { get; set; }

        /// <summary>
        /// Gets or sets the estimated cost of re-caching the context on the session model. Applies to resume and fork.
        /// </summary>
        [JsonPropertyName("estimated_cache_write_usd")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? EstimatedCacheWriteUsd { get; set; }

    }

}
