using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PreToolUse hook when the WebFetch tool is about to be invoked.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook before Claude executes the WebFetch tool,
    /// including session information, the tool input parameters, and permission context. Use this
    /// type for strongly-typed deserialization of PreToolUse hook payloads when <c>tool_name</c> is "WebFetch".
    /// </para>
    /// <para>
    /// The WebFetch tool retrieves and processes content from URLs. Your hook can inspect the target
    /// URL and prompt before the fetch occurs, enabling URL filtering or network access controls.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="WebFetchToolInput"/>) - The parameters given to the WebFetch tool</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context and tool input</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PreToolUseHookOutput{TToolInput}"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;WebFetchPreToolUsePayload&gt;(json);
    /// Console.WriteLine($"Fetching URL: {payload.ToolInput.Url}");
    /// </code>
    /// </example>
    public sealed class WebFetchPreToolUsePayload : PreToolUseHookInput<WebFetchToolInput>
    {
    }
}
