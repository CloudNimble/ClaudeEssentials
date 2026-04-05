using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the TaskStop tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The TaskStop tool stops a running background task by its ID. Your hook can inspect
    /// the task ID before the stop occurs.
    /// </para>
    /// </remarks>
    public sealed class TaskStopPreToolUsePayload : PreToolUseHookInput<TaskStopToolInput>
    {
    }
}
