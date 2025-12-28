using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a PreToolUse hook.
    /// Combines base output fields with PreToolUse-specific output.
    /// </summary>
    /// <typeparam name="TToolInput">
    /// The type representing the tool's input parameters for updates.
    /// Use a specific tool input class or <see cref="object"/> for dynamic inputs.
    /// </typeparam>
    public class PreToolUseHookOutput<TToolInput> : HookOutputBase
        where TToolInput : class
    {

        /// <summary>
        /// Gets or sets the hook-specific output containing permission decisions and input modifications.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        public PreToolUseSpecificOutput<TToolInput>? HookSpecificOutput { get; set; }

    }

}
