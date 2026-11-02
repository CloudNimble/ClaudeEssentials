using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the ClaudeDesign tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The ClaudeDesign tool performs Claude Design operations. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "ClaudeDesign".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeClaudeDesignPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class ClaudeDesignPreToolUsePayload : PreToolUseHookInput<ClaudeDesignToolInput>
    {
    }

}
