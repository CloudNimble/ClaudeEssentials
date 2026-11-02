using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the SendFeedback tool has executed.
    /// </summary>
    /// <remarks>
    /// The SendFeedback tool drafts feedback about Claude Code. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeSendFeedbackPostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class SendFeedbackPostToolUsePayload : PostToolUseHookInput<SendFeedbackToolInput, SendFeedbackToolResponse>
    {
    }

}
