using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a PostModelSwitch hook.
    /// </summary>
    public class PostModelSwitchHookOutput : HookOutputBase
    {

        #region Properties

        /// <summary>
        /// Gets or sets the hook-specific output.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PostModelSwitchSpecificOutput? HookSpecificOutput { get; set; }

        #endregion

    }

}
