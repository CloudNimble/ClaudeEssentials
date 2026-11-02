using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the Workflow tool has executed.
    /// </summary>
    /// <remarks>
    /// The Workflow tool runs a script that orchestrates multiple subagents. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeWorkflowPostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class WorkflowPostToolUsePayload : PostToolUseHookInput<WorkflowToolInput, WorkflowToolResponse>
    {
    }

}
