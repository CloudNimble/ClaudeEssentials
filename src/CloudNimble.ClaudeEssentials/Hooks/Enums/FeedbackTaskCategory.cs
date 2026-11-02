using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the kind of task a session was doing when feedback was submitted.
    /// </summary>
    public enum FeedbackTaskCategory
    {

        /// <summary>
        /// Editing code.
        /// </summary>
        [JsonStringEnumMemberName("code_edit")]
        CodeEdit,

        /// <summary>
        /// Debugging.
        /// </summary>
        [JsonStringEnumMemberName("debug")]
        Debug,

        /// <summary>
        /// Explaining something.
        /// </summary>
        [JsonStringEnumMemberName("explain")]
        Explain,

        /// <summary>
        /// Planning.
        /// </summary>
        [JsonStringEnumMemberName("plan")]
        Plan,

        /// <summary>
        /// Running shell commands.
        /// </summary>
        [JsonStringEnumMemberName("shell")]
        Shell,

        /// <summary>
        /// Searching.
        /// </summary>
        [JsonStringEnumMemberName("search")]
        Search,

        /// <summary>
        /// Reviewing.
        /// </summary>
        [JsonStringEnumMemberName("review")]
        Review,

        /// <summary>
        /// A clear task that fits no listed value.
        /// </summary>
        [JsonStringEnumMemberName("other")]
        Other

    }

}
