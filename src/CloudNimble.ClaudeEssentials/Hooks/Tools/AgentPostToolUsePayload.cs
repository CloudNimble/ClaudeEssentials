using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the Agent tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook after Claude executes the Agent tool,
    /// including session information, the original tool input, and the tool's response. Use this
    /// type for strongly-typed deserialization of PostToolUse hook payloads when <c>tool_name</c> is "Agent".
    /// </para>
    /// <para>
    /// The Agent tool launches specialized subagents. Your hook receives both the agent configuration
    /// and its completion result, enabling logging or post-processing of agent work.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;AgentPostToolUsePayload&gt;(json);
    /// Console.WriteLine($"Agent completed: {payload.ToolInput.Description}");
    /// </code>
    /// </example>
    public sealed class AgentPostToolUsePayload : PostToolUseHookInput<AgentToolInput, AgentToolResponse>
    {
    }
}
