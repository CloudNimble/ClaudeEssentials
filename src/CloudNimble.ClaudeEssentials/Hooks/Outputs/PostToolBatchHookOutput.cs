using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a PostToolBatch hook.
    /// </summary>
    public class PostToolBatchHookOutput : HookOutputBase
    {

        #region Properties

        /// <summary>
        /// Gets or sets the hook-specific output.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PostToolBatchSpecificOutput? HookSpecificOutput { get; set; }

        #endregion

    }

}
