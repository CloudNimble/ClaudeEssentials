using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the TodoWrite tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook after Claude executes the TodoWrite tool,
    /// including session information, the original tool input, and the tool's response. Use this
    /// type for strongly-typed deserialization of PostToolUse hook payloads when <c>tool_name</c> is "TodoWrite".
    /// </para>
    /// <para>
    /// The TodoWrite tool manages Claude's task list. Your hook receives both the requested changes
    /// and the before/after state of the todo list, enabling task tracking or workflow integrations.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="TodoWriteToolInput"/>) - The parameters given to the TodoWrite tool</description></item>
    /// <item><description><strong>Response</strong> (<see cref="TodoWriteToolResponse"/>) - What the TodoWrite tool produced (old and new todo state)</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context, input, and response</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PostToolUseHookOutput"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;TodoWritePostToolUsePayload&gt;(json);
    /// var completed = payload.ToolResponse.NewTodos.Count(t => t.Status == "completed");
    /// Console.WriteLine($"Todo list now has {completed} completed items");
    /// </code>
    /// </example>
    public sealed class TodoWritePostToolUsePayload : PostToolUseHookInput<TodoWriteToolInput, TodoWriteToolResponse>
    {
    }
}
