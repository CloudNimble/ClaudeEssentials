using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the action to take for an elicitation dialog response.
    /// </summary>
    public enum ElicitationAction
    {

        /// <summary>
        /// Accept the elicitation dialog with the provided form values.
        /// </summary>
        [JsonStringEnumMemberName("accept")]
        Accept,

        /// <summary>
        /// Decline the elicitation dialog.
        /// </summary>
        [JsonStringEnumMemberName("decline")]
        Decline,

        /// <summary>
        /// Cancel the elicitation dialog entirely.
        /// </summary>
        [JsonStringEnumMemberName("cancel")]
        Cancel

    }

}
