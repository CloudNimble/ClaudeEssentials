using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the Glob tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook after Claude executes the Glob tool,
    /// including session information, the original tool input, and the tool's response. Use this
    /// type for strongly-typed deserialization of PostToolUse hook payloads when <c>tool_name</c> is "Glob".
    /// </para>
    /// <para>
    /// The Glob tool searches for files matching a pattern. Your hook receives both the search
    /// parameters and the list of matching files, enabling file access logging or result filtering.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="GlobToolInput"/>) - The parameters given to the Glob tool</description></item>
    /// <item><description><strong>Response</strong> (<see cref="GlobToolResponse"/>) - What the Glob tool produced (matching file paths)</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context, input, and response</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PostToolUseHookOutput"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;GlobPostToolUsePayload&gt;(json);
    /// Console.WriteLine($"Found {payload.ToolResponse.NumFiles} files matching '{payload.ToolInput.Pattern}'");
    /// </code>
    /// </example>
    public sealed class GlobPostToolUsePayload : PostToolUseHookInput<GlobToolInput, GlobToolResponse>
    {
    }
}
