using CloudNimble.ClaudeEssentials.Hooks.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Responses;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{
    /// <summary>
    /// Represents the complete payload delivered to a PostToolUse hook after the Edit tool has executed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This payload contains all context provided to your hook after Claude executes the Edit tool,
    /// including session information, the original tool input, and the tool's response. Use this
    /// type for strongly-typed deserialization of PostToolUse hook payloads when <c>tool_name</c> is "Edit".
    /// </para>
    /// <para>
    /// The Edit tool performs string replacements in files. Your hook receives both the replacement
    /// parameters and a structured patch showing exactly what changed in the file.
    /// </para>
    /// <para>
    /// <strong>Terminology:</strong>
    /// <list type="bullet">
    /// <item><description><strong>Input</strong> (<see cref="EditToolInput"/>) - The parameters given to the Edit tool</description></item>
    /// <item><description><strong>Response</strong> (<see cref="EditToolResponse"/>) - What the Edit tool produced, including the structured patch</description></item>
    /// <item><description><strong>Payload</strong> (this class) - The complete hook delivery containing session context, input, and response</description></item>
    /// <item><description><strong>Output</strong> (<see cref="Outputs.PostToolUseHookOutput"/>) - What your hook returns to Claude</description></item>
    /// </list>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var payload = JsonSerializer.Deserialize&lt;EditPostToolUsePayload&gt;(json);
    /// Console.WriteLine($"Edited {payload.ToolResponse.FilePath}, {payload.ToolResponse.StructuredPatch.Count} hunks changed");
    /// </code>
    /// </example>
    public sealed class EditPostToolUsePayload : PostToolUseHookInput<EditToolInput, EditToolResponse>
    {
    }
}
