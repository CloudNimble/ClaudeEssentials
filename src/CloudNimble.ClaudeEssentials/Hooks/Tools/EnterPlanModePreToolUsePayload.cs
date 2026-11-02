using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the EnterPlanMode tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The EnterPlanMode tool switches the session into plan mode. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "EnterPlanMode".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeEnterPlanModePreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class EnterPlanModePreToolUsePayload : PreToolUseHookInput<EnterPlanModeToolInput>
    {
    }

}
