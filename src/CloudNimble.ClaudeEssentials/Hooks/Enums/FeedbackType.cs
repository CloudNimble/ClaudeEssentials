using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the kind of feedback submitted through the SendFeedback tool.
    /// </summary>
    public enum FeedbackType
    {

        /// <summary>
        /// A bug report.
        /// </summary>
        [JsonStringEnumMemberName("bug")]
        Bug,

        /// <summary>
        /// An idea or suggestion.
        /// </summary>
        [JsonStringEnumMemberName("idea")]
        Idea,

        /// <summary>
        /// A capability that was missing.
        /// </summary>
        [JsonStringEnumMemberName("missing_capability")]
        MissingCapability

    }

}
