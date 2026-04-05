using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a SubagentStart hook.
    /// Contains additional context to add to the subagent's context.
    /// </summary>
    public class SubagentStartSpecificOutput : HookSpecificOutputBase
    {

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.SubagentStart;

        /// <summary>
        /// Gets or sets additional context information to add to the subagent's context.
        /// This context is available to the subagent at the beginning of its execution.
        /// </summary>
        [JsonPropertyName("additionalContext")]
        public string? AdditionalContext { get; set; }

    }

}
