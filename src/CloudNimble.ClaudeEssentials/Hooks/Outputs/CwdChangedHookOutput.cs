using System.Text.Json.Serialization;
namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the output for a CwdChanged hook.
    /// This hook is observability-only and typically only uses base output fields.
    /// Environment variables can be set by writing to the CLAUDE_ENV_FILE.
    /// </summary>
    public class CwdChangedHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the hook-specific output.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CwdChangedSpecificOutput? HookSpecificOutput { get; set; }

    }

}
