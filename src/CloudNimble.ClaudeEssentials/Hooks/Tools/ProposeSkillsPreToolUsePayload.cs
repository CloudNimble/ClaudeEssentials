using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the ProposeSkills tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The ProposeSkills tool shows skill proposals on a review card. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "ProposeSkills".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeProposeSkillsPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class ProposeSkillsPreToolUsePayload : PreToolUseHookInput<ProposeSkillsToolInput>
    {
    }

}
