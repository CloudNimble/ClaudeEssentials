using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for an ElicitationResult hook.
    /// Combines base output fields with ElicitationResult-specific output.
    /// </summary>
    public class ElicitationResultHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the hook-specific output containing the elicitation result action and content.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        public ElicitationResultSpecificOutput? HookSpecificOutput { get; set; }

    }

}
