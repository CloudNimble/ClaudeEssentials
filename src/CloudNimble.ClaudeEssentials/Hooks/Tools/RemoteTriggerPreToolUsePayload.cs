using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the RemoteTrigger tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// The RemoteTrigger tool manages scheduled cloud agents. Use this type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "RemoteTrigger".
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeRemoteTriggerPreToolUse(json);
    /// var input = payload?.ToolInput;
    /// </code>
    /// </example>
    public sealed class RemoteTriggerPreToolUsePayload : PreToolUseHookInput<RemoteTriggerToolInput>
    {
    }

}
