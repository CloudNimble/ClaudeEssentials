using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the SendFeedback tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The SendFeedback tool drafts feedback about Claude Code. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "SendFeedback".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeSendFeedbackPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class SendFeedbackPreToolUsePayload : PreToolUseHookInput<SendFeedbackToolInput>
    {
    }

}
