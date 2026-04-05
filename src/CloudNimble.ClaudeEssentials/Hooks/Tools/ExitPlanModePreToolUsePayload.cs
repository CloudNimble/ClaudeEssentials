using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the ExitPlanMode tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ExitPlanMode tool transitions from plan mode to execution mode. Your hook can inspect
    /// the allowed prompts before the transition occurs.
    /// </para>
    /// </remarks>
    public sealed class ExitPlanModePreToolUsePayload : PreToolUseHookInput<ExitPlanModeToolInput>
    {
    }
}
