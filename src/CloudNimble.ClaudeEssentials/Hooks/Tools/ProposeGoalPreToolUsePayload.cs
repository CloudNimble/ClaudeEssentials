using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the ProposeGoal tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The ProposeGoal tool proposes a completion condition for the session. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "ProposeGoal".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeProposeGoalPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class ProposeGoalPreToolUsePayload : PreToolUseHookInput<ProposeGoalToolInput>
    {
    }

}
