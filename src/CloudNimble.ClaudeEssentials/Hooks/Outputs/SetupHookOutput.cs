using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a Setup hook.
    /// </summary>
    public class SetupHookOutput : HookOutputBase
    {

        #region Properties

        /// <summary>
        /// Gets or sets the hook-specific output.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SetupSpecificOutput? HookSpecificOutput { get; set; }

        #endregion

    }

}
