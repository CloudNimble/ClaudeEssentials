using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents what triggered a compact operation.
    /// </summary>
    public enum CompactTrigger
    {

        /// <summary>
        /// Compact was triggered manually by the user.
        /// </summary>
        [JsonStringEnumMemberName("manual")]
        Manual,

        /// <summary>
        /// Compact was triggered automatically by the system.
        /// </summary>
        [JsonStringEnumMemberName("auto")]
        Auto

    }

}
