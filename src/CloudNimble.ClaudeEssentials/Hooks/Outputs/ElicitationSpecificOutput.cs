using System.Text.Json.Serialization;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for an Elicitation hook.
    /// Contains the action to take and optional form field values.
    /// </summary>
    public class ElicitationSpecificOutput : HookSpecificOutputBase
    {

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.Elicitation;

        /// <summary>
        /// Gets or sets the action to take for the elicitation dialog.
        /// </summary>
        [JsonPropertyName("action")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ElicitationAction? Action { get; set; }

        /// <summary>
        /// Gets or sets the form field values when the action is <see cref="ElicitationAction.Accept"/>.
        /// The structure depends on the MCP server's elicitation schema.
        /// </summary>
        [JsonPropertyName("content")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? Content { get; set; }

    }

}
