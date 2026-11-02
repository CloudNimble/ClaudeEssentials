using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the ProposeGoal tool has executed.
    /// </summary>
    /// <remarks>
    /// The ProposeGoal tool proposes a completion condition for the session. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeProposeGoalPostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class ProposeGoalPostToolUsePayload : PostToolUseHookInput<ProposeGoalToolInput, ProposeGoalToolResponse>
    {
    }

}
