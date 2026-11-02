using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the TaskGet tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The TaskGet tool retrieves a task by its identifier. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "TaskGet".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeTaskGetPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class TaskGetPreToolUsePayload : PreToolUseHookInput<TaskGetToolInput>
    {
    }

}
