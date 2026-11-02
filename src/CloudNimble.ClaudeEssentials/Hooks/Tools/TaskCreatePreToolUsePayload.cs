using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the TaskCreate tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The TaskCreate tool adds a task to the session's task list. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "TaskCreate".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeTaskCreatePreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class TaskCreatePreToolUsePayload : PreToolUseHookInput<TaskCreateToolInput>
    {
    }

}
