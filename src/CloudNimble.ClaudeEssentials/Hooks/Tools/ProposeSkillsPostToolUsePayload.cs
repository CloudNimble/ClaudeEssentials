using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the ProposeSkills tool has executed.
    /// </summary>
    /// <remarks>
    /// The ProposeSkills tool shows skill proposals on a review card. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeProposeSkillsPostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class ProposeSkillsPostToolUsePayload : PostToolUseHookInput<ProposeSkillsToolInput, ProposeSkillsToolResponse>
    {
    }

}
