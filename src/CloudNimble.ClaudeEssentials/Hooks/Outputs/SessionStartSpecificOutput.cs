using System.Collections.Generic;
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

        /// <summary>
        /// Gets or sets the initial user message for the session.
        /// </summary>
        [JsonPropertyName("initialUserMessage")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? InitialUserMessage { get; set; }

        /// <summary>
        /// Gets or sets the session title.
        /// </summary>
        [JsonPropertyName("sessionTitle")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SessionTitle { get; set; }

        /// <summary>
        /// Gets or sets the paths to watch for changes.
        /// </summary>
        [JsonPropertyName("watchPaths")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? WatchPaths { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to re-scan skill and command directories after SessionStart hooks complete.
        /// </summary>
        [JsonPropertyName("reloadSkills")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? ReloadSkills { get; set; }

    }

}
