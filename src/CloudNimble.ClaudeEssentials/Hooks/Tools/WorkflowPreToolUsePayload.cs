using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the Workflow tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The Workflow tool runs a script that orchestrates multiple subagents. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "Workflow".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeWorkflowPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class WorkflowPreToolUsePayload : PreToolUseHookInput<WorkflowToolInput>
    {
    }

}
