using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Read tool response for an image file (PNG, JPG, GIF, WebP).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Image content is returned as base64-encoded data. Claude is a multimodal LLM and can
    /// process image data directly.
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "type": "image",
    ///   "file": {
    ///     "base64": "iVBORw0KGgo...",
    ///     "type": "image/png",
    ///     "originalSize": 24576,
    ///     "dimensions": {
    ///       "originalWidth": 1920,
    ///       "originalHeight": 1080,
    ///       "displayWidth": 960,
    ///       "displayHeight": 540
    ///     }
    ///   }
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class ImageReadToolResponse : ReadToolResponse
    {

        /// <summary>
        /// Gets or sets the image file information containing base64 data, MIME type, and dimensions.
        /// </summary>
        [JsonPropertyName("file")]
        public ImageReadToolFileInfo File { get; set; } = new();

    }

}
