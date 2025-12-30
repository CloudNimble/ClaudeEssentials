using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the WebSearch tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook after Claude executes the WebSearch tool,
    /// including session information, the original tool input, and the tool's response. Use this
    /// type for strongly-typed deserialization of PostToolUse hook payloads when <c>tool_name</c> is "WebSearch".
    /// </para>
    /// <para>
    /// The WebSearch tool searches the web for current information. Your hook receives both the search
    /// query and the results returned, enabling search logging or result filtering.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="WebSearchToolInput"/>) - The parameters given to the WebSearch tool</description></item>
    /// <item><description><strong>Response</strong> (<see cref="WebSearchToolResponse"/>) - What the WebSearch tool produced (search results)</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context, input, and response</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PostToolUseHookOutput"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;WebSearchPostToolUsePayload&gt;(json);
    /// var resultCount = payload.ToolResponse.Results.SelectMany(r => r.Content).Count();
    /// Console.WriteLine($"Found {resultCount} results for '{payload.ToolInput.Query}'");
    /// </code>
    /// </example>
    public sealed class WebSearchPostToolUsePayload : PostToolUseHookInput<WebSearchToolInput, WebSearchToolResponse>
    {
    }
}
