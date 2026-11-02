using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the ReportFindings tool, which reports code review findings as a typed list.
    /// </summary>
    public class ReportFindingsToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the verified findings, most severe first. Empty if none survived. At most 32 items.
        /// </summary>
        [JsonPropertyName("findings")]
        public List<ReviewFinding> Findings { get; set; } = [];

        /// <summary>
        /// Gets or sets the effort level the review ran at.
        /// </summary>
        [JsonPropertyName("level")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ReasoningEffort? Level { get; set; }

        #endregion

    }

}
