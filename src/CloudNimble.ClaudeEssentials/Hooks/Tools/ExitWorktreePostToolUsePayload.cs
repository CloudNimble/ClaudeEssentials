using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the ExitWorktree tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ExitWorktree tool exits and optionally removes a git worktree. Your hook receives
    /// the result including any discarded files or commits.
    /// </para>
    /// </remarks>
    public sealed class ExitWorktreePostToolUsePayload : PostToolUseHookInput<ExitWorktreeToolInput, ExitWorktreeToolResponse>
    {
    }
}
