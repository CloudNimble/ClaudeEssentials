using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the Monitor tool has executed.
    /// </summary>
    /// <remarks>
    /// The Monitor tool streams events from a background command or WebSocket. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeMonitorPostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class MonitorPostToolUsePayload : PostToolUseHookInput<MonitorToolInput, MonitorToolResponse>
    {
    }

}
