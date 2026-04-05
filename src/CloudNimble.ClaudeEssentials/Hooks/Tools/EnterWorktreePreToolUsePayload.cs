using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the EnterWorktree tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The EnterWorktree tool creates a temporary git worktree for isolated work. Your hook can
    /// inspect the optional worktree name before it is created.
    /// </para>
    /// </remarks>
    public sealed class EnterWorktreePreToolUsePayload : PreToolUseHookInput<EnterWorktreeToolInput>
    {
    }
}
