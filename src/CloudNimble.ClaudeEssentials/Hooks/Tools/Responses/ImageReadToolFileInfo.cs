using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Contains information about an image file read by the Claude Code Read tool,
    /// including base64-encoded data, MIME type, and dimension metadata.
    /// </summary>
    public class ImageReadToolFileInfo
    {

        /// <summary>
        /// Gets or sets the base64-encoded image data.
        /// </summary>
        [JsonPropertyName("base64")]
        public string Base64 { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the MIME type of the image.
        /// </summary>
        /// <remarks>
        /// Known values: <c>"image/jpeg"</c>, <c>"image/png"</c>, <c>"image/gif"</c>, <c>"image/webp"</c>.
        /// </remarks>
        [JsonPropertyName("type")]
        public ImageMediaType MimeType { get; set; }

        /// <summary>
        /// Gets or sets the original file size in bytes.
        /// </summary>
        [JsonPropertyName("originalSize")]
        public int OriginalSize { get; set; }

        /// <summary>
        /// Gets or sets the image dimension info for coordinate mapping.
        /// </summary>
        [JsonPropertyName("dimensions")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ImageDimensionInfo? Dimensions { get; set; }

    }

}
