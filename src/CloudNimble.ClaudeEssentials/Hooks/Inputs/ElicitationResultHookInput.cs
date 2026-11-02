using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by an ElicitationResult hook.
    /// This hook runs when the user responds to an elicitation dialog.
    /// </summary>
    /// <remarks>
    /// The specific fields in the elicitation result depend on the MCP server
    /// that initiated the elicitation and the user's response.
    /// </remarks>
    public class ElicitationResultHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the name of the MCP server that requested input.
        /// </summary>
        [JsonPropertyName("mcp_server_name")]
        public string McpServerName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the elicitation identifier.
        /// </summary>
        [JsonPropertyName("elicitation_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ElicitationId { get; set; }

        /// <summary>
        /// Gets or sets how input was collected.
        /// </summary>
        [JsonPropertyName("mode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ElicitationMode? Mode { get; set; }

        /// <summary>
        /// Gets or sets what the user chose to do.
        /// </summary>
        [JsonPropertyName("action")]
        public ElicitationAction Action { get; set; }

        /// <summary>
        /// Gets or sets the content the user provided.
        /// </summary>
        [JsonPropertyName("content")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, JsonElement>? Content { get; set; }

    }

}
