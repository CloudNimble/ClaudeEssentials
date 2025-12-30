using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the NotebookEdit tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook before Claude executes the NotebookEdit tool,
    /// including session information, the tool input parameters, and permission context. Use this
    /// type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "NotebookEdit".
    /// </para>
    /// <para>
    /// The NotebookEdit tool modifies Jupyter notebook cells. Your hook can inspect the notebook path,
    /// cell identifier, and new content before the edit occurs.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="NotebookEditToolInput"/>) - The parameters given to the NotebookEdit tool</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context and tool input</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PreToolUseHookOutput{TToolInput}"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;NotebookEditPreToolUsePayload&gt;(json);
    /// Console.WriteLine($"Editing notebook: {payload.ToolInput.NotebookPath}, mode: {payload.ToolInput.EditMode}");
    /// </code>
    /// </example>
    public sealed class NotebookEditPreToolUsePayload : PreToolUseHookInput<NotebookEditToolInput>
    {
    }
}
