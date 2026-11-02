using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the OfferChromeSetup tool.
    /// </summary>
    public class OfferChromeSetupToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the outcome of the offer.
        /// </summary>
        [JsonPropertyName("outcome")]
        public OfferChromeSetupOutcome Outcome { get; set; }

        #endregion

    }

}
