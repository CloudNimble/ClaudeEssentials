using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the ScheduleWakeup tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The ScheduleWakeup tool schedules when a dynamic loop resumes. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "ScheduleWakeup".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeScheduleWakeupPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class ScheduleWakeupPreToolUsePayload : PreToolUseHookInput<ScheduleWakeupToolInput>
    {
    }

}
