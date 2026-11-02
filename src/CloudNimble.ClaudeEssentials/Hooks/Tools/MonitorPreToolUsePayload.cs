using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the Monitor tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The Monitor tool streams events from a background command or WebSocket. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "Monitor".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeMonitorPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class MonitorPreToolUsePayload : PreToolUseHookInput<MonitorToolInput>
    {
    }

}
