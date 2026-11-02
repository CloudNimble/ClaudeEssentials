using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a content block in an agent's response, containing text output.
    /// </summary>
    public class AgentContentBlock
    {

        /// <summary>
        /// Gets or sets the type of content block.
        /// </summary>
        /// <remarks>
        /// See <see cref="ContentBlockType"/> for possible values.
        /// </remarks>
        [JsonPropertyName("type")]
        public ContentBlockType Type { get; set; } = ContentBlockType.Text;

        /// <summary>
        /// Gets or sets the text content of the block.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the citations attached to this block.
        /// </summary>
        [JsonPropertyName("citations")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<JsonElement>? Citations { get; set; }

    }

}
