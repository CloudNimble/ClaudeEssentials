using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Contains server-side tool use counts for an agent execution, tracking how many
    /// web search and web fetch requests were made.
    /// </summary>
    public class AgentServerToolUseInfo
    {

        /// <summary>
        /// Gets or sets the number of web search requests made.
        /// </summary>
        [JsonPropertyName("web_search_requests")]
        public int WebSearchRequests { get; set; }

        /// <summary>
        /// Gets or sets the number of web fetch requests made.
        /// </summary>
        [JsonPropertyName("web_fetch_requests")]
        public int WebFetchRequests { get; set; }

    }

}
