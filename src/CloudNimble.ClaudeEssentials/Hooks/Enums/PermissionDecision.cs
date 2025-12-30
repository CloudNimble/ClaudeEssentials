using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the decision for a PreToolUse permission check.
    /// </summary>
    public enum PermissionDecision
    {

        /// <summary>
        /// Allow the tool to execute without prompting the user.
        /// </summary>
        [JsonStringEnumMemberName("allow")]
        Allow,

        /// <summary>
        /// Deny the tool execution and inform Claude of the denial.
        /// </summary>
        [JsonStringEnumMemberName("deny")]
        Deny,

        /// <summary>
        /// Prompt the user to decide whether to allow the tool execution.
        /// </summary>
        [JsonStringEnumMemberName("ask")]
        Ask

    }

}
