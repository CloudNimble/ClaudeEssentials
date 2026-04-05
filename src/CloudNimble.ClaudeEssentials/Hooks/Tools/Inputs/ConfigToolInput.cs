using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the Config tool.
    /// The Config tool gets or sets Claude Code configuration settings.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When <see cref="Value"/> is omitted, the tool returns the current value of the setting.
    /// When <see cref="Value"/> is provided, the tool updates the setting to the new value.
    /// </para>
    /// <para>
    /// Example JSON (get):
    /// <code>
    /// { "setting": "theme" }
    /// </code>
    /// </para>
    /// <para>
    /// Example JSON (set):
    /// <code>
    /// { "setting": "permissions.defaultMode", "value": "plan" }
    /// </code>
    /// </para>
    /// </remarks>
    public class ConfigToolInput
    {

        /// <summary>
        /// Gets or sets the setting key (e.g., "theme", "model", "permissions.defaultMode").
        /// </summary>
        [JsonPropertyName("setting")]
        public string Setting { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new value. Omit to get the current value.
        /// </summary>
        /// <remarks>
        /// The value can be a string, boolean, or number depending on the setting.
        /// </remarks>
        [JsonPropertyName("value")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? Value { get; set; }

    }

}
