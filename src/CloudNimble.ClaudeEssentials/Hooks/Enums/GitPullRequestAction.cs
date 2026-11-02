using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents a pull request operation detected by the Bash tool.
    /// </summary>
    public enum GitPullRequestAction
    {

        /// <summary>
        /// The pull request was created.
        /// </summary>
        [JsonStringEnumMemberName("created")]
        Created,

        /// <summary>
        /// The pull request was edited.
        /// </summary>
        [JsonStringEnumMemberName("edited")]
        Edited,

        /// <summary>
        /// The pull request was merged.
        /// </summary>
        [JsonStringEnumMemberName("merged")]
        Merged,

        /// <summary>
        /// A comment was added to the pull request.
        /// </summary>
        [JsonStringEnumMemberName("commented")]
        Commented,

        /// <summary>
        /// The pull request was closed.
        /// </summary>
        [JsonStringEnumMemberName("closed")]
        Closed,

        /// <summary>
        /// The pull request was reopened.
        /// </summary>
        [JsonStringEnumMemberName("reopened")]
        Reopened,

        /// <summary>
        /// The pull request was marked ready for review.
        /// </summary>
        [JsonStringEnumMemberName("ready")]
        Ready,

        /// <summary>
        /// The pull request was converted to a draft.
        /// </summary>
        [JsonStringEnumMemberName("draft")]
        Draft,

        /// <summary>
        /// Auto-merge was enabled on the pull request.
        /// </summary>
        [JsonStringEnumMemberName("auto-merge-enabled")]
        AutoMergeEnabled,

        /// <summary>
        /// Auto-merge was disabled on the pull request.
        /// </summary>
        [JsonStringEnumMemberName("auto-merge-disabled")]
        AutoMergeDisabled

    }

}
