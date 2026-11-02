using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents what happened to a code review finding when fixes were applied.
    /// </summary>
    public enum ReviewFindingOutcome
    {

        /// <summary>
        /// The finding was fixed.
        /// </summary>
        [JsonStringEnumMemberName("fixed")]
        Fixed,

        /// <summary>
        /// The finding was skipped.
        /// </summary>
        [JsonStringEnumMemberName("skipped")]
        Skipped,

        /// <summary>
        /// No change turned out to be needed.
        /// </summary>
        [JsonStringEnumMemberName("no_change_needed")]
        NoChangeNeeded

    }

}
