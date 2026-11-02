using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the ShowOnboardingRolePicker tool.
    /// </summary>
    public class ShowOnboardingRolePickerToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the user dismissed the picker.
        /// </summary>
        [JsonPropertyName("dismissed")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Dismissed { get; set; }

        /// <summary>
        /// Gets or sets the role the user selected.
        /// </summary>
        [JsonPropertyName("role")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Role { get; set; }

        #endregion

    }

}
