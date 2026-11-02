using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a Setup hook, which runs during repository setup or maintenance.
    /// </summary>
    public class SetupHookInput : HookInputBase
    {

        #region Properties

        /// <summary>
        /// Gets or sets what triggered the hook.
        /// </summary>
        [JsonPropertyName("trigger")]
        public SetupTrigger Trigger { get; set; }

        #endregion

    }

}
