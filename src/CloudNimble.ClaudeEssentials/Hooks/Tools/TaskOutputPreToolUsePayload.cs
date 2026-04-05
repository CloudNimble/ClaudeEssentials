using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the TaskOutput tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The TaskOutput tool retrieves output from a background task by its ID. Your hook can inspect
    /// the task ID and wait parameters before the output is retrieved.
    /// </para>
    /// </remarks>
    public sealed class TaskOutputPreToolUsePayload : PreToolUseHookInput<TaskOutputToolInput>
    {
    }
}
