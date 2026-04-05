using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents a hook's decision to block an operation.
    /// </summary>
    /// <remarks>
    /// In hook outputs, set <c>"decision": "block"</c> to prevent the operation.
    /// Omit the <c>decision</c> field entirely to allow the operation to proceed.
    /// </remarks>
    public enum HookDecision
    {

        /// <summary>
        /// Allow the operation to proceed normally.
        /// </summary>
        [JsonStringEnumMemberName("allow")]
        Allow,

        /// <summary>
        /// Block the operation from proceeding.
        /// </summary>
        [JsonStringEnumMemberName("block")]
        Block

    }

}
