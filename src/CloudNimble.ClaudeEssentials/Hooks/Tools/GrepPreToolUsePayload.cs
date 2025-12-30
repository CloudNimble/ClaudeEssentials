using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the Grep tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook before Claude executes the Grep tool,
    /// including session information, the tool input parameters, and permission context. Use this
    /// type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "Grep".
    /// </para>
    /// <para>
    /// The Grep tool searches file contents using regex patterns. Your hook can inspect the search
    /// pattern and target path before the search occurs, enabling content access controls.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="GrepToolInput"/>) - The parameters given to the Grep tool</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context and tool input</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PreToolUseHookOutput{TToolInput}"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;GrepPreToolUsePayload&gt;(json);
    /// Console.WriteLine($"Searching for pattern: {payload.ToolInput.Pattern}");
    /// </code>
    /// </example>
    public sealed class GrepPreToolUsePayload : PreToolUseHookInput<GrepToolInput>
    {
    }
}
