using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the ExitWorktree tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ExitWorktree tool exits and optionally removes a git worktree. Your hook can inspect
    /// the action (keep or remove) and discard_changes flag before the operation occurs.
    /// </para>
    /// </remarks>
    public sealed class ExitWorktreePreToolUsePayload : PreToolUseHookInput<ExitWorktreeToolInput>
    {
    }
}
