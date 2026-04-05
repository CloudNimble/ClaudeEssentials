using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Contains image dimension information for coordinate mapping, including both original
    /// and displayed sizes after any resizing.
    /// </summary>
    public class ImageDimensionInfo
    {

        /// <summary>
        /// Gets or sets the original image width in pixels.
        /// </summary>
        [JsonPropertyName("originalWidth")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? OriginalWidth { get; set; }

        /// <summary>
        /// Gets or sets the original image height in pixels.
        /// </summary>
        [JsonPropertyName("originalHeight")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? OriginalHeight { get; set; }

        /// <summary>
        /// Gets or sets the displayed image width in pixels (after resizing).
        /// </summary>
        [JsonPropertyName("displayWidth")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? DisplayWidth { get; set; }

        /// <summary>
        /// Gets or sets the displayed image height in pixels (after resizing).
        /// </summary>
        [JsonPropertyName("displayHeight")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? DisplayHeight { get; set; }

    }

}
