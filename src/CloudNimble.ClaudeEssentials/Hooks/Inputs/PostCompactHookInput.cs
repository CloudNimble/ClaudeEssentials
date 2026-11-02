using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a PostCompact hook.
    /// This hook runs after a compact operation completes.
    /// </summary>
    public class PostCompactHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets what triggered the compact operation.
        /// </summary>
        [JsonPropertyName("trigger")]
        public CompactTrigger Trigger { get; set; }

        /// <summary>
        /// Gets or sets the conversation summary produced by compaction.
        /// </summary>
        [JsonPropertyName("compact_summary")]
        public string CompactSummary { get; set; } = string.Empty;

    }

}
