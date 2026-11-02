using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the kind of change detected on a watched file.
    /// </summary>
    public enum FileChangeEvent
    {

        /// <summary>
        /// The file changed.
        /// </summary>
        [JsonStringEnumMemberName("change")]
        Change,

        /// <summary>
        /// The file was added.
        /// </summary>
        [JsonStringEnumMemberName("add")]
        Add,

        /// <summary>
        /// The file was removed.
        /// </summary>
        [JsonStringEnumMemberName("unlink")]
        Unlink

    }

}
