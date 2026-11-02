using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the EnterPlanMode tool has executed.
    /// </summary>
    /// <remarks>
    /// The EnterPlanMode tool switches the session into plan mode. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeEnterPlanModePostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class EnterPlanModePostToolUsePayload : PostToolUseHookInput<EnterPlanModeToolInput, EnterPlanModeToolResponse>
    {
    }

}
