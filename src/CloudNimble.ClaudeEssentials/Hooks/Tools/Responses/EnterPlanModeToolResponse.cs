using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the EnterPlanMode tool.
    /// </summary>
    public class EnterPlanModeToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the confirmation that plan mode was entered.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        #endregion

    }

}
