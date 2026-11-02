using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the result of a verification pass over a code review finding.
    /// </summary>
    public enum ReviewFindingVerdict
    {

        /// <summary>
        /// The finding was confirmed by verification.
        /// </summary>
        [JsonStringEnumMemberName("CONFIRMED")]
        Confirmed,

        /// <summary>
        /// The finding is plausible but was not fully confirmed.
        /// </summary>
        [JsonStringEnumMemberName("PLAUSIBLE")]
        Plausible

    }

}
