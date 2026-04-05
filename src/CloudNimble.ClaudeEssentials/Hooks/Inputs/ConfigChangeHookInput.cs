using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a ConfigChange hook.
    /// This hook runs when a configuration setting changes.
    /// </summary>
    public class ConfigChangeHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the source of the configuration change.
        /// </summary>
        [JsonPropertyName("source")]
        public ConfigSource Source { get; set; }

    }

}
