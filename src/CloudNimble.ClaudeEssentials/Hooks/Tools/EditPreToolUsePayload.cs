using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the Edit tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook before Claude executes the Edit tool,
    /// including session information, the tool input parameters, and permission context. Use this
    /// type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "Edit".
    /// </para>
    /// <para>
    /// The Edit tool performs string replacements in files. Your hook can inspect the target file,
    /// the string being replaced, and the replacement before the edit occurs.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="EditToolInput"/>) - The parameters given to the Edit tool</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context and tool input</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PreToolUseHookOutput{TToolInput}"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;EditPreToolUsePayload&gt;(json);
    /// Console.WriteLine($"Editing {payload.ToolInput.FilePath}: replacing '{payload.ToolInput.OldString}'");
    /// </code>
    /// </example>
    public sealed class EditPreToolUsePayload : PreToolUseHookInput<EditToolInput>
    {
    }
}
