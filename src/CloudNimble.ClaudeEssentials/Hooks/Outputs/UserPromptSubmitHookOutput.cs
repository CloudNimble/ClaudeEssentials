using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a UserPromptSubmit hook.
    /// Combines base output fields with UserPromptSubmit-specific output.
    /// </summary>
    public class UserPromptSubmitHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the hook-specific output containing additional context.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        public UserPromptSubmitSpecificOutput? HookSpecificOutput { get; set; }

    }

}
