using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the TaskStop tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The TaskStop tool stops a running background task. Your hook receives both the
    /// request parameters and the result, including the stopped task's type and command.
    /// </para>
    /// </remarks>
    public sealed class TaskStopPostToolUsePayload : PostToolUseHookInput<TaskStopToolInput, TaskStopToolResponse>
    {
    }
}
