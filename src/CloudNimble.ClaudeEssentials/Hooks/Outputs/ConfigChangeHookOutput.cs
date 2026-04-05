using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the output for a ConfigChange hook.
    /// Used to optionally block configuration changes (except policy_settings).
    /// </summary>
    public class ConfigChangeHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the decision for the configuration change.
        /// Set to <see cref="HookDecision.Block"/> to prevent the configuration change.
        /// Cannot block changes from <see cref="ConfigSource.PolicySettings"/>.
        /// </summary>
        [JsonPropertyName("decision")]
        public HookDecision? Decision { get; set; }

        /// <summary>
        /// Gets or sets the reason for blocking the configuration change.
        /// Required when blocking to explain why the change was rejected.
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

    }

}
