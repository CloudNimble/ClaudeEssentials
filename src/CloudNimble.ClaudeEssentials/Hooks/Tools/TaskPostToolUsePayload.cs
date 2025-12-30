using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the Task tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook after Claude executes the Task tool,
    /// including session information, the original tool input, and the tool's response. Use this
    /// type for strongly-typed deserialization of PostToolUse hook payloads when <c>tool_name</c> is "Task".
    /// </para>
    /// <para>
    /// The Task tool launches subagents to handle complex tasks. Your hook receives both the task
    /// parameters and the agent's result, enabling logging of agent activity or result processing.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="TaskToolInput"/>) - The parameters given to the Task tool</description></item>
    /// <item><description><strong>Response</strong> (<see cref="TaskToolResponse"/>) - What the Task tool produced (agent result and ID)</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context, input, and response</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PostToolUseHookOutput"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;TaskPostToolUsePayload&gt;(json);
    /// Console.WriteLine($"Agent {payload.ToolResponse.AgentId} completed with status: {payload.ToolResponse.Status}");
    /// </code>
    /// </example>
    public sealed class TaskPostToolUsePayload : PostToolUseHookInput<TaskToolInput, TaskToolResponse>
    {
    }
}
