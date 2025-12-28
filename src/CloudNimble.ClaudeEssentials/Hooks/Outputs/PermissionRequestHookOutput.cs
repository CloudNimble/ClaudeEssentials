using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a PermissionRequest hook.
    /// Combines base output fields with PermissionRequest-specific output.
    /// </summary>
    /// <typeparam name="TToolInput">
    /// The type representing the tool's input parameters for updates.
    /// Use a specific tool input class or <see cref="object"/> for dynamic inputs.
    /// </typeparam>
    public class PermissionRequestHookOutput<TToolInput> : HookOutputBase
        where TToolInput : class
    {

        /// <summary>
        /// Gets or sets the hook-specific output containing the permission decision.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        public PermissionRequestSpecificOutput<TToolInput>? HookSpecificOutput { get; set; }

    }

}
