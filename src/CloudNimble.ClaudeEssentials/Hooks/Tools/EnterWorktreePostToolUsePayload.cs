using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the EnterWorktree tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The EnterWorktree tool creates a temporary git worktree. Your hook receives
    /// the worktree path and branch information after creation.
    /// </para>
    /// </remarks>
    public sealed class EnterWorktreePostToolUsePayload : PostToolUseHookInput<EnterWorktreeToolInput, EnterWorktreeToolResponse>
    {
    }
}
