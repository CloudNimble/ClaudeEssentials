using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the reasoning effort level of an agent or review.
    /// </summary>
    public enum ReasoningEffort
    {

        /// <summary>
        /// Low effort.
        /// </summary>
        [JsonStringEnumMemberName("low")]
        Low,

        /// <summary>
        /// Medium effort.
        /// </summary>
        [JsonStringEnumMemberName("medium")]
        Medium,

        /// <summary>
        /// High effort.
        /// </summary>
        [JsonStringEnumMemberName("high")]
        High,

        /// <summary>
        /// Extra-high effort.
        /// </summary>
        [JsonStringEnumMemberName("xhigh")]
        XHigh,

        /// <summary>
        /// Maximum effort.
        /// </summary>
        [JsonStringEnumMemberName("max")]
        Max

    }

}
