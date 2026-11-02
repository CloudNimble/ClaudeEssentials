using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by an Elicitation hook.
    /// This hook runs when an elicitation dialog is presented to the user.
    /// </summary>
    /// <remarks>
    /// The specific fields in the elicitation request depend on the MCP server
    /// that initiated the elicitation.
    /// </remarks>
    public class ElicitationHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the name of the MCP server requesting input.
        /// </summary>
        [JsonPropertyName("mcp_server_name")]
        public string McpServerName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the message to show the user.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets how input is collected.
        /// </summary>
        [JsonPropertyName("mode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ElicitationMode? Mode { get; set; }

        /// <summary>
        /// Gets or sets the URL to send the user to in URL mode.
        /// </summary>
        [JsonPropertyName("url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the elicitation identifier.
        /// </summary>
        [JsonPropertyName("elicitation_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ElicitationId { get; set; }

        /// <summary>
        /// Gets or sets the JSON schema of the requested input.
        /// </summary>
        [JsonPropertyName("requested_schema")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, JsonElement>? RequestedSchema { get; set; }

    }

}
