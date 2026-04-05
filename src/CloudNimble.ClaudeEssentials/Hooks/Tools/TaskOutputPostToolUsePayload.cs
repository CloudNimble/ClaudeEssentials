using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the TaskOutput tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The TaskOutput tool retrieves output from a background task. Your hook receives both the
    /// request parameters and the task's output, enabling logging or post-processing.
    /// </para>
    /// </remarks>
    public sealed class TaskOutputPostToolUsePayload : PostToolUseHookInput<TaskOutputToolInput, TaskToolResponse>
    {
    }
}
