using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a FileChanged hook.
    /// </summary>
    public class FileChangedSpecificOutput : HookSpecificOutputBase
    {

        #region Properties

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.FileChanged;

        /// <summary>
        /// Gets or sets the paths to watch for changes.
        /// </summary>
        [JsonPropertyName("watchPaths")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? WatchPaths { get; set; }

        #endregion

    }

}
