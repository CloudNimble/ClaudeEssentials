using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the output for a Stop hook.
    /// Used to optionally block Claude from stopping and continue processing.
    /// </summary>
    public class StopHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the hook-specific output.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public StopSpecificOutput? HookSpecificOutput { get; set; }

    }

}
