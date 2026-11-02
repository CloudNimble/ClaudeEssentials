using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the behavior of a permission rule.
    /// </summary>
    public enum PermissionBehavior
    {

        /// <summary>
        /// The rule allows the action.
        /// </summary>
        [JsonStringEnumMemberName("allow")]
        Allow,

        /// <summary>
        /// The rule denies the action.
        /// </summary>
        [JsonStringEnumMemberName("deny")]
        Deny,

        /// <summary>
        /// The rule asks the user.
        /// </summary>
        [JsonStringEnumMemberName("ask")]
        Ask

    }

}
