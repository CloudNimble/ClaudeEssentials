using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the PushNotification tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The PushNotification tool sends a notification to the user. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "PushNotification".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializePushNotificationPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class PushNotificationPreToolUsePayload : PreToolUseHookInput<PushNotificationToolInput>
    {
    }

}
