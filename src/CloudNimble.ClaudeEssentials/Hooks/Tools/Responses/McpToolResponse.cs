using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Converters;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the result of an MCP tool execution.
    /// </summary>
    /// <remarks>
    /// The result is an untagged union, so exactly one of <see cref="Text"/>, <see cref="ContentBlocks"/>, or
    /// <see cref="StructuredContent"/> is populated, according to the JSON shape that the tool returned.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeMcpPostToolUse(json);
    /// var text = payload?.ToolResponse?.Text;
    /// </code>
    /// </example>
    [JsonConverter(typeof(McpToolResponseConverter))]
    public class McpToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the content blocks, when the tool returned an array.
        /// </summary>
        public List<McpContentBlock>? ContentBlocks { get; set; }

        /// <summary>
        /// Gets or sets the structured result, when the tool returned an object.
        /// </summary>
        public Dictionary<string, JsonElement>? StructuredContent { get; set; }

        /// <summary>
        /// Gets or sets the text result, when the tool returned a string.
        /// </summary>
        public string? Text { get; set; }

        #endregion

    }

}
