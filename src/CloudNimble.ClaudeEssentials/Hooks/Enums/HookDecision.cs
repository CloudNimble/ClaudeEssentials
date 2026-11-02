using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the top-level decision a hook returns.
    /// </summary>
    public enum HookDecision
    {

        /// <summary>
        /// Approves the action.
        /// </summary>
        [JsonStringEnumMemberName("approve")]
        Approve,

        /// <summary>
        /// Blocks the action.
        /// </summary>
        [JsonStringEnumMemberName("block")]
        Block

    }

}
