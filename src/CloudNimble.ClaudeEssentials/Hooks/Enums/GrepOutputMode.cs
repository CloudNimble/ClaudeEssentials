using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the output mode for grep search results. <see cref="FilesWithMatches"/> is the default.
    /// </summary>
    public enum GrepOutputMode
    {

        /// <summary>
        /// Returns only the paths of files that contain at least one match. This is the default output mode.
        /// </summary>
        [JsonStringEnumMemberName("files_with_matches")]
        FilesWithMatches,

        /// <summary>
        /// Returns the matching lines from each file along with their file paths.
        /// </summary>
        [JsonStringEnumMemberName("content")]
        Content,

        /// <summary>
        /// Returns the number of matches found in each file.
        /// </summary>
        [JsonStringEnumMemberName("count")]
        Count

    }

}
