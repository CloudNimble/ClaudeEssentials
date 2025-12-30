using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{
    /// <summary>
    /// Represents the response payload returned by the Claude Code WebSearch tool after
    /// performing a web search.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The WebSearch tool allows Claude to search the web for up-to-date information beyond
    /// its training data cutoff. This response is received in the
    /// <see cref="WebSearchPostToolUsePayload"/> when the
    /// <c>tool_name</c> is "WebSearch".
    /// </para>
    /// <para>
    /// Note: Web search is currently only available in the US region.
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "query": "Claude Code documentation 2025",
    ///   "results": [
    ///     {
    ///       "tool_use_id": "srvtoolu_01ABC123...",
    ///       "content": [
    ///         {
    ///           "title": "Claude Code Documentation",
    ///           "url": "https://docs.anthropic.com/claude-code"
    ///         },
    ///         {
    ///           "title": "Getting Started with Claude Code",
    ///           "url": "https://anthropic.com/claude-code/getting-started"
    ///         }
    ///       ]
    ///     }
    ///   ],
    ///   "durationSeconds": 2.45
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class WebSearchToolResponse
    {
        /// <summary>
        /// Gets or sets the search query that was executed.
        /// </summary>
        /// <remarks>
        /// This is the exact query string that was sent to the search engine.
        /// It matches the <see cref="WebSearchToolInput.Query"/> from the input.
        /// </remarks>
        [JsonPropertyName("query")]
        public string Query { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the search results returned from the web search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Results are grouped into containers, each containing multiple search result items.
        /// The structure allows for multiple result sets from a single search operation.
        /// </para>
        /// <para>
        /// Use these results to inform responses about current events, recent documentation,
        /// or other information that may have changed since Claude's training data cutoff.
        /// </para>
        /// </remarks>
        [JsonPropertyName("results")]
        public List<WebSearchResultContainer> Results { get; set; } = [];

        /// <summary>
        /// Gets or sets the total duration of the search operation in seconds.
        /// </summary>
        /// <remarks>
        /// This includes the time taken to perform the web search and process the results.
        /// Web searches typically complete within a few seconds, but may take longer
        /// depending on network conditions and search complexity.
        /// </remarks>
        [JsonPropertyName("durationSeconds")]
        public double DurationSeconds { get; set; }
    }

    /// <summary>
    /// Represents a container grouping web search results from a single search operation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Search results are organized into containers that can be traced back to specific
    /// tool invocations using the <see cref="ToolUseId"/>. Each container holds multiple
    /// individual search result items.
    /// </para>
    /// </remarks>
    public class WebSearchResultContainer
    {
        /// <summary>
        /// Gets or sets the unique identifier for the tool use that produced these results.
        /// </summary>
        /// <remarks>
        /// This ID can be used to correlate search results with specific tool invocations
        /// in the conversation history. It follows the format <c>"srvtoolu_"</c> followed
        /// by a unique identifier string.
        /// </remarks>
        /// <example>
        /// <code>
        /// "srvtoolu_01Y7zVgSRg9cAvqgZdue61h3"
        /// </code>
        /// </example>
        [JsonPropertyName("tool_use_id")]
        public string ToolUseId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of individual search result items in this container.
        /// </summary>
        /// <remarks>
        /// Each item represents a single search result with a title and URL.
        /// Results are ordered by relevance as determined by the search engine.
        /// </remarks>
        [JsonPropertyName("content")]
        public List<WebSearchResultItem> Content { get; set; } = [];
    }

    /// <summary>
    /// Represents an individual web search result with a title and URL.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each search result item provides the essential information needed to understand
    /// what the result is about (<see cref="Title"/>) and where to find more information
    /// (<see cref="Url"/>).
    /// </para>
    /// <para>
    /// The URL can be used with the <see cref="WebFetchToolInput"/> to retrieve the
    /// full content of the page for more detailed information.
    /// </para>
    /// </remarks>
    public class WebSearchResultItem
    {
        /// <summary>
        /// Gets or sets the title of the search result.
        /// </summary>
        /// <remarks>
        /// This is typically the page title or heading from the search result.
        /// It provides a brief description of what the linked page contains.
        /// </remarks>
        /// <example>
        /// <code>
        /// "Claude Code Documentation - Anthropic"
        /// </code>
        /// </example>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL of the search result.
        /// </summary>
        /// <remarks>
        /// This is the direct link to the search result page. The URL can be used
        /// with <see cref="WebFetchToolInput"/> to retrieve and process the page content.
        /// </remarks>
        /// <example>
        /// <code>
        /// "https://docs.anthropic.com/claude-code/overview"
        /// </code>
        /// </example>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}
