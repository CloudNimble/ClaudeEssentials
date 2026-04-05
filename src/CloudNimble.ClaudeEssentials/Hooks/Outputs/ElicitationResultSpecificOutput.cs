using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for an ElicitationResult hook.
    /// Contains the action to take and optional override form field values.
    /// </summary>
    public class ElicitationResultSpecificOutput : HookSpecificOutputBase
    {

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.ElicitationResult;

        /// <summary>
        /// Gets or sets the action to take for the elicitation result.
        /// </summary>
        [JsonPropertyName("action")]
        public ElicitationAction Action { get; set; }

        /// <summary>
        /// Gets or sets override form field values.
        /// The structure depends on the MCP server's elicitation schema.
        /// </summary>
        [JsonPropertyName("content")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? Content { get; set; }

    }

}
