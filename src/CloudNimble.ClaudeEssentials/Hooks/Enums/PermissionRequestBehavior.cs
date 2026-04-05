using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the behavior decision for a PermissionRequest hook.
    /// </summary>
    public enum PermissionRequestBehavior
    {

        /// <summary>
        /// Allow the permission request and proceed with the operation.
        /// </summary>
        [JsonStringEnumMemberName("allow")]
        Allow,

        /// <summary>
        /// Deny the permission request and block the operation.
        /// </summary>
        [JsonStringEnumMemberName("deny")]
        Deny

    }

}
