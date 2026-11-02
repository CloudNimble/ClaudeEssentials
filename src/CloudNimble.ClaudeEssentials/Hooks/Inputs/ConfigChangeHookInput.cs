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

        /// <summary>
        /// Gets or sets the path of the configuration file that changed.
        /// </summary>
        [JsonPropertyName("file_path")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FilePath { get; set; }

    }

}
