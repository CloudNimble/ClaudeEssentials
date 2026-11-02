using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the time to live of the prompt cache.
    /// </summary>
    public enum PromptCacheTtl
    {

        /// <summary>
        /// Five minutes.
        /// </summary>
        [JsonStringEnumMemberName("5m")]
        FiveMinutes,

        /// <summary>
        /// One hour.
        /// </summary>
        [JsonStringEnumMemberName("1h")]
        OneHour

    }

}
