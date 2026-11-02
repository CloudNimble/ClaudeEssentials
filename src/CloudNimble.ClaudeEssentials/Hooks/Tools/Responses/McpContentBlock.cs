using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents a content block returned by an MCP tool.
    /// </summary>
    /// <remarks>
    /// Only <see cref="Type"/> is defined by the schema. Every other property depends on the block type and is available through <see cref="ExtensionData"/>.
    /// </remarks>
    public class McpContentBlock
    {

        #region Properties

        /// <summary>
        /// Gets or sets any additional properties of the content block.
        /// </summary>
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? ExtensionData { get; set; }

        /// <summary>
        /// Gets or sets the type of the content block.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        #endregion

    }

}
