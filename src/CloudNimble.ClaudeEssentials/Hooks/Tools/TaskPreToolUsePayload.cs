using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the Task tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook before Claude executes the Task tool,
    /// including session information, the tool input parameters, and permission context. Use this
    /// type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "Task".
    /// </para>
    /// <para>
    /// The Task tool launches subagents to handle complex tasks. Your hook can inspect the task
    /// description, agent type, and model before the subagent is spawned.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="TaskToolInput"/>) - The parameters given to the Task tool</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context and tool input</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PreToolUseHookOutput{TToolInput}"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;TaskPreToolUsePayload&gt;(json);
    /// Console.WriteLine($"Launching {payload.ToolInput.SubagentType} agent: {payload.ToolInput.Description}");
    /// </code>
    /// </example>
    public sealed class TaskPreToolUsePayload : PreToolUseHookInput<TaskToolInput>
    {
    }
}
