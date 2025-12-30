using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a SessionStart hook.
    /// Contains additional context to add to the session start.
    /// </summary>
    public class SessionStartSpecificOutput : HookSpecificOutputBase
    {

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.SessionStart;

        /// <summary>
        /// Gets or sets additional context information to add to the session start.
        /// This context is available to Claude at the beginning of the session.
        /// </summary>
        [JsonPropertyName("additionalContext")]
        public string? AdditionalContext { get; set; }

    }

}
