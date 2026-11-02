using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the output of a hook that continues running asynchronously in the background.
    /// </summary>
    public class AsyncHookOutput
    {

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the hook runs asynchronously. The schema only allows <see langword="true"/>.
        /// </summary>
        [JsonPropertyName("async")]
        public bool Async { get; set; } = true;

        /// <summary>
        /// Gets or sets the optional timeout for the asynchronous hook.
        /// </summary>
        [JsonPropertyName("asyncTimeout")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? AsyncTimeout { get; set; }

        #endregion

    }

}
