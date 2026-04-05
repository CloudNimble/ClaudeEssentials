using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the Claude model family used in a session or tool invocation.
    /// </summary>
    public enum ClaudeModel
    {

        /// <summary>
        /// The Claude Sonnet model, balancing capability and speed.
        /// </summary>
        [JsonStringEnumMemberName("sonnet")]
        Sonnet,

        /// <summary>
        /// The Claude Opus model, optimized for the most demanding tasks.
        /// </summary>
        [JsonStringEnumMemberName("opus")]
        Opus,

        /// <summary>
        /// The Claude Haiku model, optimized for speed and efficiency.
        /// </summary>
        [JsonStringEnumMemberName("haiku")]
        Haiku

    }

}
