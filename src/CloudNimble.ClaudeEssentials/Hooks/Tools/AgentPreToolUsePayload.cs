using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the Agent tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook before Claude executes the Agent tool,
    /// including session information, the tool input parameters, and permission context. Use this
    /// type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "Agent".
    /// </para>
    /// <para>
    /// The Agent tool launches specialized subagents. Your hook can inspect the agent configuration
    /// before it is spawned, enabling policy enforcement or logging.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;AgentPreToolUsePayload&gt;(json);
    /// Console.WriteLine($"Spawning agent: {payload.ToolInput.Description}");
    /// </code>
    /// </example>
    public sealed class AgentPreToolUsePayload : PreToolUseHookInput<AgentToolInput>
    {
    }
}
