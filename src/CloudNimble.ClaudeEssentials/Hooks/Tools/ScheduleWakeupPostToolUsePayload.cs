using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the ScheduleWakeup tool has executed.
    /// </summary>
    /// <remarks>
    /// The ScheduleWakeup tool schedules when a dynamic loop resumes. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeScheduleWakeupPostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class ScheduleWakeupPostToolUsePayload : PostToolUseHookInput<ScheduleWakeupToolInput, ScheduleWakeupToolResponse>
    {
    }

}
