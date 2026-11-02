using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the RemoteTrigger tool has executed.
    /// </summary>
    /// <remarks>
    /// The RemoteTrigger tool manages scheduled cloud agents. Your hook receives both the request parameters and the result.
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = ClaudeHooksSerializer.DeserializeRemoteTriggerPostToolUse(json);
    /// var response = payload?.ToolResponse;
    /// </code>
    /// </example>
    public sealed class RemoteTriggerPostToolUsePayload : PostToolUseHookInput<RemoteTriggerToolInput, RemoteTriggerToolResponse>
    {
    }

}
