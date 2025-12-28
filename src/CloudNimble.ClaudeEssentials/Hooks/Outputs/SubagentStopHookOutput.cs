using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Enums;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the output for a SubagentStop hook.
    /// Used to optionally block a subagent from stopping and continue processing.
    /// </summary>
    public class SubagentStopHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the decision for the subagent stop operation.
        /// Set to <see cref="HookDecision.Block"/> to prevent the subagent from stopping.
        /// </summary>
        [JsonPropertyName("decision")]
        public HookDecision? Decision { get; set; }

        /// <summary>
        /// Gets or sets the reason for blocking the stop operation.
        /// Required when blocking to explain why the subagent should continue.
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

    }

}
