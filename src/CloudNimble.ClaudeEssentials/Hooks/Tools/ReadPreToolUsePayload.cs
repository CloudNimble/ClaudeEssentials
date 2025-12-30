using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the Read tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook before Claude executes the Read tool,
    /// including session information, the tool input parameters, and permission context. Use this
    /// type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "Read".
    /// </para>
    /// <para>
    /// The Read tool reads files from the local filesystem. Your hook can inspect the file path
    /// and other parameters before the read occurs, potentially blocking or modifying the operation.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="ReadToolInput"/>) - The parameters given to the Read tool</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context and tool input</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PreToolUseHookOutput{TToolInput}"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;ReadPreToolUsePayload&gt;(json);
    /// Console.WriteLine($"Reading file: {payload.ToolInput.FilePath}");
    /// </code>
    /// </example>
    public sealed class ReadPreToolUsePayload : PreToolUseHookInput<ReadToolInput>
    {
    }
}
