using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the Config tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Config tool gets or sets Claude Code configuration settings. Your hook can inspect
    /// the setting key and value before the operation occurs.
    /// </para>
    /// </remarks>
    public sealed class ConfigPreToolUsePayload : PreToolUseHookInput<ConfigToolInput>
    {
    }
}
