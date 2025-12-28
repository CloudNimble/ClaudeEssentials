using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a SubagentStop hook.
    /// This hook runs when subagent tasks complete.
    /// </summary>
    public class SubagentStopHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets a value indicating whether the stop hook is already active.
        /// True if already continuing from a previous stop hook.
        /// </summary>
        [JsonPropertyName("stop_hook_active")]
        public bool StopHookActive { get; set; }

    }

}
