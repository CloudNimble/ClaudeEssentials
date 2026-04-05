using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the AskUserQuestion tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The AskUserQuestion tool presents structured multiple-choice questions to the user.
    /// Your hook can inspect or modify the questions before they are presented.
    /// </para>
    /// </remarks>
    public sealed class AskUserQuestionPreToolUsePayload : PreToolUseHookInput<AskUserQuestionToolInput>
    {
    }
}
