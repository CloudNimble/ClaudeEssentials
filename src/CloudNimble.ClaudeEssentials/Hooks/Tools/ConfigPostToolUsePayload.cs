using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the Config tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Config tool gets or sets Claude Code configuration settings. Your hook receives
    /// both the request parameters and the result, enabling logging or policy enforcement.
    /// </para>
    /// </remarks>
    public sealed class ConfigPostToolUsePayload : PostToolUseHookInput<ConfigToolInput, ConfigToolResponse>
    {
    }
}
