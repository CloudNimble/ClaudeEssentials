using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a PreModelSwitch hook.
    /// </summary>
    public class PreModelSwitchHookOutput : HookOutputBase
    {

        #region Properties

        /// <summary>
        /// Gets or sets the hook-specific output.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PreModelSwitchSpecificOutput? HookSpecificOutput { get; set; }

        #endregion

    }

}
