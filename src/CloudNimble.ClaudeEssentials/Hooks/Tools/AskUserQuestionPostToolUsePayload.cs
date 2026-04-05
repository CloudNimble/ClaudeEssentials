using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the AskUserQuestion tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The AskUserQuestion tool presents structured multiple-choice questions to the user.
    /// Your hook receives both the questions and the user's answers, enabling logging or validation.
    /// </para>
    /// </remarks>
    public sealed class AskUserQuestionPostToolUsePayload : PostToolUseHookInput<AskUserQuestionToolInput, AskUserQuestionToolResponse>
    {
    }
}
