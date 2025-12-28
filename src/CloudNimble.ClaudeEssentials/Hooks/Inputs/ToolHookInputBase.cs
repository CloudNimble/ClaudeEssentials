using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Base class for tool-related hook inputs that contain tool name, input, and use ID.
    /// Used as a base for PreToolUse and PostToolUse hook inputs.
    /// </summary>
    /// <typeparam name="TToolInput">
    /// The type representing the tool's input parameters.
    /// Use a specific tool input class or <see cref="object"/> for dynamic inputs.
    /// </typeparam>
    public abstract class ToolHookInputBase<TToolInput> : HookInputBase
        where TToolInput : class
    {

        /// <summary>
        /// Gets or sets the name of the tool being invoked.
        /// Common tool names include: Write, Edit, Bash, Read, Grep, Glob, Task, WebFetch, WebSearch.
        /// MCP tools follow the pattern: mcp__&lt;server&gt;__&lt;tool&gt;.
        /// </summary>
        [JsonPropertyName("tool_name")]
        public string ToolName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the input parameters for the tool.
        /// The schema depends on the specific tool being invoked.
        /// </summary>
        [JsonPropertyName("tool_input")]
        public TToolInput? ToolInput { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for this specific tool use instance.
        /// Typically follows the pattern: toolu_01ABC123...
        /// </summary>
        [JsonPropertyName("tool_use_id")]
        public string ToolUseId { get; set; } = string.Empty;

    }

}
