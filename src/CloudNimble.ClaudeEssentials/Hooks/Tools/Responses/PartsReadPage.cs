using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a page image extracted from a PDF by the Read tool.
    /// </summary>
    public class PartsReadPage
    {

        #region Properties

        /// <summary>
        /// Gets or sets the Base64-encoded page image. Empty when the page could not be processed.
        /// </summary>
        [JsonPropertyName("base64")]
        public string Base64 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets why the page could not be processed as an image. Set only when <c>base64</c> is empty.
        /// </summary>
        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; }

        /// <summary>
        /// Gets or sets the MIME type of the image.
        /// </summary>
        [JsonPropertyName("mediaType")]
        public ImageMediaType MediaType { get; set; }

        #endregion

    }

}
