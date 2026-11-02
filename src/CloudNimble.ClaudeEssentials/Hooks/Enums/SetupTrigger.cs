using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents what triggered a Setup hook.
    /// </summary>
    public enum SetupTrigger
    {

        /// <summary>
        /// Initial setup.
        /// </summary>
        [JsonStringEnumMemberName("init")]
        Init,

        /// <summary>
        /// Repository maintenance.
        /// </summary>
        [JsonStringEnumMemberName("maintenance")]
        Maintenance

    }

}
