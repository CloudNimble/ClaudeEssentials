using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the diff status of a file in a git operation.
    /// </summary>
    public enum GitDiffStatus
    {

        /// <summary>
        /// The file has been modified from its previous state.
        /// </summary>
        [JsonStringEnumMemberName("modified")]
        Modified,

        /// <summary>
        /// The file has been newly added and was not previously tracked.
        /// </summary>
        [JsonStringEnumMemberName("added")]
        Added

    }

}
