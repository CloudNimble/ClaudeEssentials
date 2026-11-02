using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the output for a SubagentStop hook.
    /// Used to optionally block a subagent from stopping and continue processing.
    /// </summary>
    public class SubagentStopHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the hook-specific output.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SubagentStopSpecificOutput? HookSpecificOutput { get; set; }

    }

}
