using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the WebFetch tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook after Claude executes the WebFetch tool,
    /// including session information, the original tool input, and the tool's response. Use this
    /// type for strongly-typed deserialization of PostToolUse hook payloads when <c>tool_name</c> is "WebFetch".
    /// </para>
    /// <para>
    /// The WebFetch tool retrieves and processes content from URLs. Your hook receives both the URL
    /// and prompt that was used, plus the processed content that was returned.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="WebFetchToolInput"/>) - The parameters given to the WebFetch tool</description></item>
    /// <item><description><strong>Response</strong> (<see cref="WebFetchToolResponse"/>) - What the WebFetch tool produced (processed content)</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context, input, and response</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PostToolUseHookOutput"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;WebFetchPostToolUsePayload&gt;(json);
    /// Console.WriteLine($"Fetched {payload.ToolInput.Url} in {payload.ToolResponse.DurationSeconds}s");
    /// </code>
    /// </example>
    public sealed class WebFetchPostToolUsePayload : PostToolUseHookInput<WebFetchToolInput, WebFetchToolResponse>
    {
    }
}
