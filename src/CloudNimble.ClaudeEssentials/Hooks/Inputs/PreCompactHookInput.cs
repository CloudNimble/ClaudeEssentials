using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a PreCompact hook.
    /// This hook runs before a compact operation.
    /// </summary>
    public class PreCompactHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets what triggered the compact operation.
        /// </summary>
        [JsonPropertyName("trigger")]
        public CompactTrigger Trigger { get; set; }

        /// <summary>
        /// Gets or sets custom instructions for the compact operation.
        /// Only set when the trigger is <see cref="CompactTrigger.Manual"/>.
        /// </summary>
        [JsonPropertyName("custom_instructions")]
        public string? CustomInstructions { get; set; }

    }

}
