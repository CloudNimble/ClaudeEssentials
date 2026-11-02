using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the ProposeSkills tool.
    /// </summary>
    public class ProposeSkillsToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the number of proposals shown on the review card.
        /// </summary>
        [JsonPropertyName("proposalCount")]
        public int ProposalCount { get; set; }

        #endregion

    }

}
