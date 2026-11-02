using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the OfferChromeSetup tool.
    /// </summary>
    public class OfferChromeSetupToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets a short phrase naming what the task needs the user's own browser for.
        /// </summary>
        [JsonPropertyName("reason")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Reason { get; set; }

        #endregion

    }

}
