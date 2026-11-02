using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the CronDelete tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The CronDelete tool removes a scheduled job. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "CronDelete".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeCronDeletePreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class CronDeletePreToolUsePayload : PreToolUseHookInput<CronDeleteToolInput>
    {
    }

}
