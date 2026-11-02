using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the TaskList tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The TaskList tool lists the tasks in the session. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "TaskList".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeTaskListPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class TaskListPreToolUsePayload : PreToolUseHookInput<TaskListToolInput>
    {
    }

}
