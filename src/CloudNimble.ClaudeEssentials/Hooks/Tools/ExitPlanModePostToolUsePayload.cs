using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the ExitPlanMode tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ExitPlanMode tool transitions from plan mode to execution mode. Your hook receives
    /// the plan content and metadata about the transition.
    /// </para>
    /// </remarks>
    public sealed class ExitPlanModePostToolUsePayload : PostToolUseHookInput<ExitPlanModeToolInput, ExitPlanModeToolResponse>
    {
    }
}
