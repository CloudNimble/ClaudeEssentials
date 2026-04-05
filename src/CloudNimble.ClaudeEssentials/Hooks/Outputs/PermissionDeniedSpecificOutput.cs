using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a PermissionDenied hook.
    /// Contains the option to allow the model to retry the denied permission.
    /// </summary>
    public class PermissionDeniedSpecificOutput : HookSpecificOutputBase
    {

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.PermissionDenied;

        /// <summary>
        /// Gets or sets a value indicating whether to tell the model it may retry.
        /// Set to true to signal that the model should attempt the permission request again.
        /// </summary>
        [JsonPropertyName("retry")]
        public bool Retry { get; set; }

    }

}
