using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the type of write operation performed on a file.
    /// </summary>
    public enum WriteOperationType
    {

        /// <summary>
        /// Creates a new file that does not yet exist.
        /// </summary>
        [JsonStringEnumMemberName("create")]
        Create,

        /// <summary>
        /// Overwrites the full content of an existing file.
        /// </summary>
        [JsonStringEnumMemberName("overwrite")]
        Overwrite

    }

}
