using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the type of a content block within a Claude message or response.
    /// </summary>
    public enum ContentBlockType
    {

        /// <summary>
        /// A plain-text content block.
        /// </summary>
        [JsonStringEnumMemberName("text")]
        Text

    }

}
