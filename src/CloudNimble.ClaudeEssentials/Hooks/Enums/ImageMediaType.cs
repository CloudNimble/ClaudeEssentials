using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the MIME type of an image returned by the Read tool.
    /// </summary>
    public enum ImageMediaType
    {

        /// <summary>
        /// A JPEG image.
        /// </summary>
        [JsonStringEnumMemberName("image/jpeg")]
        Jpeg,

        /// <summary>
        /// A PNG image.
        /// </summary>
        [JsonStringEnumMemberName("image/png")]
        Png,

        /// <summary>
        /// A GIF image.
        /// </summary>
        [JsonStringEnumMemberName("image/gif")]
        Gif,

        /// <summary>
        /// A WebP image.
        /// </summary>
        [JsonStringEnumMemberName("image/webp")]
        Webp

    }

}
