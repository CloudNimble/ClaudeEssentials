using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the ReadNotifications tool has executed.
    /// </summary>
    /// <remarks>
    /// The ReadNotifications tool drains the session's notification queue. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeReadNotificationsPostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class ReadNotificationsPostToolUsePayload : PostToolUseHookInput<ReadNotificationsToolInput, ReadNotificationsToolResponse>
    {
    }

}
