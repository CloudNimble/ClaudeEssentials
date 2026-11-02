using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the ProposeGoal tool.
    /// </summary>
    public class ProposeGoalToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the user was asked for approval rather than the goal being set directly.
        /// </summary>
        [JsonPropertyName("askUser")]
        public bool AskUser { get; set; }

        /// <summary>
        /// Gets or sets the condition shown to the user for approval, or set directly.
        /// </summary>
        [JsonPropertyName("condition")]
        public string Condition { get; set; } = string.Empty;

        #endregion

    }

}
