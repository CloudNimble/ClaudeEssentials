using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the ReadNotifications tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The ReadNotifications tool drains the session's notification queue. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "ReadNotifications".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeReadNotificationsPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class ReadNotificationsPreToolUsePayload : PreToolUseHookInput<ReadNotificationsToolInput>
    {
    }

}
