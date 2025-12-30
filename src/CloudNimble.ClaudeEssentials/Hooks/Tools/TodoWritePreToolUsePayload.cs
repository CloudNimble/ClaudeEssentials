using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the TodoWrite tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook before Claude executes the TodoWrite tool,
    /// including session information, the tool input parameters, and permission context. Use this
    /// type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "TodoWrite".
    /// </para>
    /// <para>
    /// The TodoWrite tool manages Claude's task list. Your hook can inspect the todo items
    /// before they are updated, enabling task tracking or workflow integrations.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="TodoWriteToolInput"/>) - The parameters given to the TodoWrite tool</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context and tool input</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PreToolUseHookOutput{TToolInput}"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;TodoWritePreToolUsePayload&gt;(json);
    /// Console.WriteLine($"Updating {payload.ToolInput.Todos.Count} todo items");
    /// </code>
    /// </example>
    public sealed class TodoWritePreToolUsePayload : PreToolUseHookInput<TodoWriteToolInput>
    {
    }
}
