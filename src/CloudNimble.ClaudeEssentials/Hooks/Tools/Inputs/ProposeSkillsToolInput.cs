using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the ProposeSkills tool, which shows skill proposals on a review card.
    /// </summary>
    public class ProposeSkillsToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the skill proposals. Between 1 and 3 items.
        /// </summary>
        [JsonPropertyName("proposals")]
        public List<SkillProposal> Proposals { get; set; } = [];

        #endregion

    }

}
