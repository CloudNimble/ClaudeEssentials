using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the Grep tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook after Claude executes the Grep tool,
    /// including session information, the original tool input, and the tool's response. Use this
    /// type for strongly-typed deserialization of PostToolUse hook payloads when <c>tool_name</c> is "Grep".
    /// </para>
    /// <para>
    /// The Grep tool searches file contents using regex patterns. Your hook receives both the search
    /// parameters and the results (files, content, or counts depending on mode).
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="GrepToolInput"/>) - The parameters given to the Grep tool</description></item>
    /// <item><description><strong>Response</strong> (<see cref="GrepToolResponse"/>) - What the Grep tool produced (files, content, or counts)</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context, input, and response</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PostToolUseHookOutput"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;GrepPostToolUsePayload&gt;(json);
    /// Console.WriteLine($"Found matches in {payload.ToolResponse.NumFiles} files for '{payload.ToolInput.Pattern}'");
    /// </code>
    /// </example>
    public sealed class GrepPostToolUsePayload : PostToolUseHookInput<GrepToolInput, GrepToolResponse>
    {
    }
}
