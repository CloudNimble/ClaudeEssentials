using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the NotebookEdit tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook after Claude executes the NotebookEdit tool,
    /// including session information, the original tool input, and the tool's response. Use this
    /// type for strongly-typed deserialization of PostToolUse hook payloads when <c>tool_name</c> is "NotebookEdit".
    /// </para>
    /// <para>
    /// The NotebookEdit tool modifies Jupyter notebook cells. Your hook receives both the edit
    /// parameters and the result, enabling notebook change tracking.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="NotebookEditToolInput"/>) - The parameters given to the NotebookEdit tool</description></item>
    /// <item><description><strong>Response</strong> (<see cref="NotebookEditToolResponse"/>) - What the NotebookEdit tool produced (success status)</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context, input, and response</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PostToolUseHookOutput"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;NotebookEditPostToolUsePayload&gt;(json);
    /// if (payload.ToolResponse.Success)
    ///     Console.WriteLine($"Successfully edited cell in {payload.ToolResponse.NotebookPath}");
    /// </code>
    /// </example>
    public sealed class NotebookEditPostToolUsePayload : PostToolUseHookInput<NotebookEditToolInput, NotebookEditToolResponse>
    {
    }
}
