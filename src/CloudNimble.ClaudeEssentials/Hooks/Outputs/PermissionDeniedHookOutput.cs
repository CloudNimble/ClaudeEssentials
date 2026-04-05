using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a PermissionDenied hook.
    /// Combines base output fields with PermissionDenied-specific output.
    /// </summary>
    public class PermissionDeniedHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the hook-specific output containing retry information.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        public PermissionDeniedSpecificOutput? HookSpecificOutput { get; set; }

    }

}
