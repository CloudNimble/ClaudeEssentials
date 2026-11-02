using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the SendFeedback tool.
    /// </summary>
    public class SendFeedbackToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the result message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the feedback was accepted.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        #endregion

    }

}
