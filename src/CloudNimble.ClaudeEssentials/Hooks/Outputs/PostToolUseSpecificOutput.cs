using System.Text.Json.Serialization;
using System.Text.Json;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a PostToolUse hook.
    /// Contains additional context to provide to Claude after tool execution.
    /// </summary>
    public class PostToolUseSpecificOutput : HookSpecificOutputBase
    {

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.PostToolUse;

        /// <summary>
        /// Gets or sets additional context information to provide to Claude.
        /// This information is added to Claude's context about the tool execution.
        /// </summary>
        [JsonPropertyName("additionalContext")]
        public string? AdditionalContext { get; set; }

        /// <summary>
        /// Gets or sets host-asserted context shown to the auto-mode permission classifier alongside this tool call's result.
        /// </summary>
        [JsonPropertyName("classifierContext")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ClassifierContext { get; set; }

        /// <summary>
        /// Gets or sets the value that replaces the tool output before it is sent to the model.
        /// </summary>
        [JsonPropertyName("updatedToolOutput")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? UpdatedToolOutput { get; set; }

        /// <summary>
        /// Gets or sets the value that replaces the output for MCP tools only. Prefer <c>updatedToolOutput</c>, which works for all tools.
        /// </summary>
        [JsonPropertyName("updatedMCPToolOutput")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? UpdatedMCPToolOutput { get; set; }

    }

}
