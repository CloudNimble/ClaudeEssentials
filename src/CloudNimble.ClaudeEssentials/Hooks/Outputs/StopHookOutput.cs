using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Enums;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the output for a Stop hook.
    /// Used to optionally block Claude from stopping and continue processing.
    /// </summary>
    public class StopHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the decision for the stop operation.
        /// Set to <see cref="HookDecision.Block"/> to prevent Claude from stopping.
        /// </summary>
        [JsonPropertyName("decision")]
        public HookDecision? Decision { get; set; }

        /// <summary>
        /// Gets or sets the reason for blocking the stop operation.
        /// Required when blocking to explain why Claude should continue.
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

    }

}
