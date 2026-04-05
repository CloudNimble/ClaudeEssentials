using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for an Elicitation hook.
    /// Combines base output fields with Elicitation-specific output.
    /// </summary>
    public class ElicitationHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the hook-specific output containing the elicitation action and content.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        public ElicitationSpecificOutput? HookSpecificOutput { get; set; }

    }

}
