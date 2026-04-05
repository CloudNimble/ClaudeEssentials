using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a SubagentStart hook.
    /// This hook runs when a subagent task starts.
    /// </summary>
    public class SubagentStartHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the unique identifier for the subagent.
        /// </summary>
        [JsonPropertyName("agent_id")]
        public string AgentId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the subagent (e.g., "Bash", "Explore", "Plan", or a custom type).
        /// </summary>
        [JsonPropertyName("agent_type")]
        public string AgentType { get; set; } = string.Empty;

    }

}
