using CloudNimble.ClaudeEssentials.Hooks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the Claude Code Config tool after
    /// getting or setting a configuration value.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Example JSON payload (get):
    /// <code>
    /// {
    ///   "success": true,
    ///   "operation": "get",
    ///   "setting": "theme",
    ///   "value": "dark"
    /// }
    /// </code>
    /// </para>
    /// <para>
    /// Example JSON payload (set):
    /// <code>
    /// {
    ///   "success": true,
    ///   "operation": "set",
    ///   "setting": "theme",
    ///   "previousValue": "light",
    ///   "newValue": "dark"
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class ConfigToolResponse
    {

        /// <summary>
        /// Gets or sets whether the operation succeeded.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the type of operation performed.
        /// </summary>
        /// <remarks>
        /// See <see cref="ConfigOperation"/> for possible values.
        /// </remarks>
        [JsonPropertyName("operation")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ConfigOperation? Operation { get; set; }

        /// <summary>
        /// Gets or sets the setting key that was accessed.
        /// </summary>
        [JsonPropertyName("setting")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Setting { get; set; }

        /// <summary>
        /// Gets or sets the current value of the setting (for get operations).
        /// </summary>
        [JsonPropertyName("value")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? Value { get; set; }

        /// <summary>
        /// Gets or sets the previous value before the update (for set operations).
        /// </summary>
        [JsonPropertyName("previousValue")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? PreviousValue { get; set; }

        /// <summary>
        /// Gets or sets the new value after the update (for set operations).
        /// </summary>
        [JsonPropertyName("newValue")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? NewValue { get; set; }

        /// <summary>
        /// Gets or sets the error message if the operation failed.
        /// </summary>
        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; }

    }

}
