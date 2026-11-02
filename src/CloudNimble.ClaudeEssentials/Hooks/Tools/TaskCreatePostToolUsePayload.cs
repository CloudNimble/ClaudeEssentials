using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the TaskCreate tool has executed.
    /// </summary>
    /// <remarks>
    /// The TaskCreate tool adds a task to the session's task list. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeTaskCreatePostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class TaskCreatePostToolUsePayload : PostToolUseHookInput<TaskCreateToolInput, TaskCreateToolResponse>
    {
    }

}
