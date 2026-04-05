using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the type of operation performed on a Claude Code configuration entry.
    /// </summary>
    public enum ConfigOperation
    {

        /// <summary>
        /// Retrieves the current value of a configuration entry.
        /// </summary>
        [JsonStringEnumMemberName("get")]
        Get,

        /// <summary>
        /// Sets or updates the value of a configuration entry.
        /// </summary>
        [JsonStringEnumMemberName("set")]
        Set

    }

}
