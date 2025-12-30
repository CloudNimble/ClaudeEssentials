using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a PreToolUse hook.
    /// Contains permission decisions and optional input modifications.
    /// </summary>
    /// <typeparam name="TToolInput">
    /// The type representing the tool's input parameters for updates.
    /// Use a specific tool input class or <see cref="object"/> for dynamic inputs.
    /// </typeparam>
    public class PreToolUseSpecificOutput<TToolInput> : HookSpecificOutputBase
        where TToolInput : class
    {

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.PreToolUse;

        /// <summary>
        /// Gets or sets the permission decision for the tool execution.
        /// </summary>
        [JsonPropertyName("permissionDecision")]
        public PermissionDecision? PermissionDecision { get; set; }

        /// <summary>
        /// Gets or sets the reason for the permission decision.
        /// This message is shown to Claude to explain why the tool was allowed, denied, or requires user input.
        /// </summary>
        [JsonPropertyName("permissionDecisionReason")]
        public string? PermissionDecisionReason { get; set; }

        /// <summary>
        /// Gets or sets optional modifications to the tool's input parameters.
        /// Only the fields that need to be changed should be included.
        /// </summary>
        [JsonPropertyName("updatedInput")]
        public TToolInput? UpdatedInput { get; set; }

    }

}
