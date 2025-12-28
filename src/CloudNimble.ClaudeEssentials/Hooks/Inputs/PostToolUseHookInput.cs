using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a PostToolUse hook.
    /// This hook runs after tool calls complete and includes the tool's response.
    /// </summary>
    /// <typeparam name="TToolInput">
    /// The type representing the tool's input parameters.
    /// Use a specific tool input class or <see cref="object"/> for dynamic inputs.
    /// </typeparam>
    /// <typeparam name="TToolResponse">
    /// The type representing the tool's response data.
    /// Use a specific tool response class or <see cref="object"/> for dynamic responses.
    /// </typeparam>
    public class PostToolUseHookInput<TToolInput, TToolResponse> : ToolHookInputBase<TToolInput>
        where TToolInput : class
        where TToolResponse : class
    {

        /// <summary>
        /// Gets or sets the response data returned by the tool.
        /// The schema depends on the specific tool that was invoked.
        /// </summary>
        [JsonPropertyName("tool_response")]
        public TToolResponse? ToolResponse { get; set; }

    }

}
