using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the WebSearch tool.
    /// The WebSearch tool allows Claude to search the web and use the results to inform responses.
    /// </summary>
    /// <remarks>
    /// Provides up-to-date information for current events and recent data.
    /// Web search is only available in the US.
    /// </remarks>
    public class WebSearchToolInput
    {

        /// <summary>
        /// Gets or sets the search query to use.
        /// </summary>
        /// <remarks>
        /// Minimum 2 characters.
        /// </remarks>
        [JsonPropertyName("query")]
        public string Query { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the domains to include in search results.
        /// </summary>
        /// <remarks>
        /// Only include search results from these domains.
        /// </remarks>
        [JsonPropertyName("allowed_domains")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[]? AllowedDomains { get; set; }

        /// <summary>
        /// Gets or sets the domains to exclude from search results.
        /// </summary>
        /// <remarks>
        /// Never include search results from these domains.
        /// </remarks>
        [JsonPropertyName("blocked_domains")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string[]? BlockedDomains { get; set; }

    }

}
