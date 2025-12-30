using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a PermissionRequest hook.
    /// Contains the decision object with behavior and optional parameters.
    /// </summary>
    /// <typeparam name="TToolInput">
    /// The type representing the tool's input parameters for updates.
    /// Use a specific tool input class or <see cref="object"/> for dynamic inputs.
    /// </typeparam>
    public class PermissionRequestSpecificOutput<TToolInput> : HookSpecificOutputBase
        where TToolInput : class
    {

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.PermissionRequest;

        /// <summary>
        /// Gets or sets the decision object containing the behavior and related options.
        /// </summary>
        [JsonPropertyName("decision")]
        public PermissionRequestDecision<TToolInput>? Decision { get; set; }

    }

}
