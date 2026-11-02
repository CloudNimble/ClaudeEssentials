using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the ClaudeDesign tool has executed.
    /// </summary>
    /// <remarks>
    /// The ClaudeDesign tool performs Claude Design operations. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeClaudeDesignPostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class ClaudeDesignPostToolUsePayload : PostToolUseHookInput<ClaudeDesignToolInput, ClaudeDesignToolResponse>
    {
    }

}
