using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the CronCreate tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The CronCreate tool schedules a prompt on a cron expression. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "CronCreate".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeCronCreatePreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class CronCreatePreToolUsePayload : PreToolUseHookInput<CronCreateToolInput>
    {
    }

}
