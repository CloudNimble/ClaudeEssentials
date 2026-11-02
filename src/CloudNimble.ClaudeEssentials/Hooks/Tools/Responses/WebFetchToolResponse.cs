using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

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
    /// </list>
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "url": "https://docs.anthropic.com/claude-code",
    ///   "bytes": 45210,
    ///   "code": 200,
    ///   "codeText": "OK",
    ///   "result": "Claude Code is a CLI tool...",
    ///   "durationMs": 1250
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
        /// </remarks>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of bytes in the raw response body before processing.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This indicates the size of the original content fetched from the URL before
        /// any markdown conversion or summarization was applied.
        /// </para>
        /// <para>
        /// This property may be <c>null</c> for older-style responses that do not include
        /// byte-level metrics.
        /// </para>
        /// </remarks>
        [JsonPropertyName("bytes")]
        public long Bytes { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code returned by the server.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Common values include <c>200</c> (OK), <c>301</c> (Moved Permanently),
        /// <c>404</c> (Not Found), etc.
        /// </para>
        /// <para>
        /// This property may be <c>null</c> for older-style responses that do not include
        /// the HTTP status code.
        /// </para>
        /// </remarks>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status text corresponding to <see cref="Code"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// For example, <c>"OK"</c> for a 200 response or <c>"Moved Permanently"</c> for a 301 redirect.
        /// </para>
        /// <para>
        /// This property may be <c>null</c> for older-style responses that do not include
        /// the HTTP status text.
        /// </para>
        /// </remarks>
        [JsonPropertyName("codeText")]
        public string CodeText { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the duration of the fetch and processing operation in milliseconds.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This includes the time taken to fetch the URL content, convert HTML to markdown,
        /// and process the content with the specified prompt.
        /// </para>
        /// <para>
        /// This property may be <c>null</c> when duration information is not available.
        /// </para>
        /// </remarks>
        [JsonPropertyName("durationMs")]
        public int DurationMs { get; set; }

        /// <summary>
        /// Gets or sets the processed result text from the fetched URL.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the result of applying the prompt to the fetched content.
        /// </para>
        /// </remarks>
        [JsonPropertyName("result")]
        public string Result { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the artifact and version that now count as viewed.
        /// </summary>
        [JsonPropertyName("artifactRead")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactReadInfo? ArtifactRead { get; set; }

    }
}
