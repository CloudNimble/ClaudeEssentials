using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents one tool call in a resolved batch.
    /// </summary>
    public class PostToolBatchToolCall
    {

        #region Properties

        /// <summary>
        /// Gets or sets the input parameters for the tool.
        /// </summary>
        [JsonPropertyName("tool_input")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? ToolInput { get; set; }

        /// <summary>
        /// Gets or sets the name of the tool.
        /// </summary>
        [JsonPropertyName("tool_name")]
        public string ToolName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the response returned by the tool.
        /// </summary>
        [JsonPropertyName("tool_response")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? ToolResponse { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for this tool use.
        /// </summary>
        [JsonPropertyName("tool_use_id")]
        public string ToolUseId { get; set; } = string.Empty;

        #endregion

    }

}
