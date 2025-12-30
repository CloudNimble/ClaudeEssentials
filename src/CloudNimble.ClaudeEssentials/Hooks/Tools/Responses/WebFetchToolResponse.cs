using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{
    /// <summary>
    /// Represents the response payload returned by the Claude Code WebFetch tool after
    /// fetching and processing content from a URL.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The WebFetch tool retrieves content from a specified URL, converts HTML to markdown,
    /// and processes it using a small, fast model based on the provided prompt. This response
    /// is received in the <see cref="WebFetchPostToolUsePayload"/>
    /// when the <c>tool_name</c> is "WebFetch".
    /// </para>
    /// <para>
    /// Key features of the WebFetch tool:
    /// <list type="bullet">
    /// <item><description>HTTP URLs are automatically upgraded to HTTPS</description></item>
    /// <item><description>Content is converted from HTML to markdown for easier processing</description></item>
    /// <item><description>Large content may be summarized or truncated</description></item>
    /// <item><description>Includes a 15-minute cache for repeated requests to the same URL</description></item>
    /// <item><description>Redirects to different hosts are reported rather than followed automatically</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "url": "https://docs.anthropic.com/claude-code",
    ///   "content": "# Claude Code Documentation\n\nClaude Code is a CLI tool...",
    ///   "durationSeconds": 1.25,
    ///   "truncated": false
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class WebFetchToolResponse
    {
        /// <summary>
        /// Gets or sets the URL that was fetched.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the actual URL that was requested. If the original URL used HTTP,
        /// it will have been upgraded to HTTPS.
        /// </para>
        /// <para>
        /// If a redirect occurred to a different host, this will still show the original
        /// URL, and <see cref="RedirectUrl"/> will contain the redirect destination.
        /// </para>
        /// </remarks>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the processed content from the fetched URL.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The content is the result of processing the URL with the prompt specified in
        /// <see cref="WebFetchToolInput.Prompt"/>. HTML content is converted to markdown
        /// before processing.
        /// </para>
        /// <para>
        /// If the content was very large, it may have been summarized by the processing
        /// model. Check <see cref="Truncated"/> to determine if truncation occurred.
        /// </para>
        /// </remarks>
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the duration of the fetch and processing operation in seconds.
        /// </summary>
        /// <remarks>
        /// This includes the time taken to:
        /// <list type="bullet">
        /// <item><description>Fetch the URL content (or retrieve from cache)</description></item>
        /// <item><description>Convert HTML to markdown</description></item>
        /// <item><description>Process the content with the specified prompt</description></item>
        /// </list>
        /// Cached responses (within the 15-minute cache window) will typically be faster.
        /// </remarks>
        [JsonPropertyName("durationSeconds")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public double? DurationSeconds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the content was truncated due to size limits.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When <c>true</c>, the original content exceeded the maximum size and was
        /// truncated or summarized. The <see cref="Content"/> will contain a condensed
        /// version of the information.
        /// </para>
        /// <para>
        /// For very large pages, consider using a more specific prompt in
        /// <see cref="WebFetchToolInput.Prompt"/> to extract only the relevant information.
        /// </para>
        /// </remarks>
        [JsonPropertyName("truncated")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Truncated { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL if the fetch resulted in a redirect to a different host.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When a URL redirects to a different host (domain), the WebFetch tool does not
        /// automatically follow the redirect. Instead, it reports the redirect URL here
        /// so that a new fetch request can be made explicitly.
        /// </para>
        /// <para>
        /// This behavior provides transparency about where content is coming from and
        /// prevents unexpected cross-domain requests.
        /// </para>
        /// <para>
        /// If no cross-host redirect occurred, this property will be <c>null</c>.
        /// Same-host redirects (e.g., HTTP to HTTPS on the same domain) are followed automatically.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Original URL: https://short.link/abc
        /// // Redirect URL: https://example.com/full-article
        /// </code>
        /// </example>
        [JsonPropertyName("redirectUrl")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RedirectUrl { get; set; }
    }
}
