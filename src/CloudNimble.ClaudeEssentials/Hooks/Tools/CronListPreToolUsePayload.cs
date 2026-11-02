using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the CronList tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The CronList tool lists the scheduled jobs. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "CronList".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeCronListPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class CronListPreToolUsePayload : PreToolUseHookInput<CronListToolInput>
    {
    }

}
