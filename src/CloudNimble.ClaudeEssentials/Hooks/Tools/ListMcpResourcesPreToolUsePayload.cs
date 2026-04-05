using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the ListMcpResources tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ListMcpResources tool lists available MCP resources. Your hook can inspect
    /// the optional server filter before the listing occurs.
    /// </para>
    /// </remarks>
    public sealed class ListMcpResourcesPreToolUsePayload : PreToolUseHookInput<ListMcpResourcesToolInput>
    {
    }
}
