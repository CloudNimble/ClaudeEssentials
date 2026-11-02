using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a MessageDisplay hook.
    /// </summary>
    public class MessageDisplayHookOutput : HookOutputBase
    {

        #region Properties

        /// <summary>
        /// Gets or sets the hook-specific output.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MessageDisplaySpecificOutput? HookSpecificOutput { get; set; }

        #endregion

    }

}
