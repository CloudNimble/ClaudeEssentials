using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a PostToolBatch hook, which runs once after every tool call in a batch has resolved.
    /// </summary>
    public class PostToolBatchHookInput : HookInputBase
    {

        #region Properties

        /// <summary>
        /// Gets or sets the tool calls in the batch.
        /// </summary>
        [JsonPropertyName("tool_calls")]
        public List<PostToolBatchToolCall> ToolCalls { get; set; } = [];

        #endregion

    }

}
