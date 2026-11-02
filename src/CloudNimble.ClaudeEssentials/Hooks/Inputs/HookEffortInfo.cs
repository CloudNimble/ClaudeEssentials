using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the reasoning effort applied to the current turn.
    /// </summary>
    public class HookEffortInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the active effort level for the current turn, after any silent downgrade for the selected model.
        /// </summary>
        [JsonPropertyName("level")]
        public string Level { get; set; } = string.Empty;

        #endregion

    }

}
