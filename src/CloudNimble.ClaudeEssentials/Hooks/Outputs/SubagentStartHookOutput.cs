using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a SubagentStart hook.
    /// Combines base output fields with SubagentStart-specific output.
    /// </summary>
    public class SubagentStartHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the hook-specific output containing additional context for the subagent.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        public SubagentStartSpecificOutput? HookSpecificOutput { get; set; }

    }

}
