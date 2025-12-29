using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the input parameters for the WebFetch tool.
    /// The WebFetch tool fetches content from a URL and processes it using an AI model.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Fetches URL content, converts HTML to markdown, and processes the content with a prompt
    /// using a small, fast model. Includes a self-cleaning 15-minute cache.
    /// </para>
    /// </remarks>
    public class WebFetchToolInput
    {

        /// <summary>
        /// Gets or sets the URL to fetch content from.
        /// </summary>
        /// <remarks>
        /// Must be a fully-formed valid URL. HTTP URLs will be automatically upgraded to HTTPS.
        /// </remarks>
        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the prompt to run on the fetched content.
        /// </summary>
        /// <remarks>
        /// Describes what information to extract from the page.
        /// </remarks>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

    }

}
