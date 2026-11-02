using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the ReportFindings tool.
    /// </summary>
    public class ReportFindingsToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the number of findings reported.
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the findings, echoed for the result body.
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
