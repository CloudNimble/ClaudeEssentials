using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents a single code review finding.
    /// </summary>
    public class ReviewFinding
    {

        #region Properties

        /// <summary>
        /// Gets or sets a short kebab-case slug of the finding type, such as <c>correctness</c> or <c>test-coverage</c>.
        /// </summary>
        [JsonPropertyName("category")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the concrete inputs or state that lead to wrong output or a crash.
        /// </summary>
        [JsonPropertyName("failure_scenario")]
        public string FailureScenario { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the repo-relative path of the file the finding is in.
        /// </summary>
        [JsonPropertyName("file")]
        public string File { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the 1-indexed line the finding anchors to.
        /// </summary>
        [JsonPropertyName("line")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Line { get; set; }

        /// <summary>
        /// Gets or sets what happened to this finding. Set only when re-reporting after applying fixes.
        /// </summary>
        [JsonPropertyName("outcome")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ReviewFindingOutcome? Outcome { get; set; }

        /// <summary>
        /// Gets or sets a compressed label for compact UI (at most 60 characters): the claim alone, with no rationale or consequence clause.
        /// </summary>
        [JsonPropertyName("short_summary")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ShortSummary { get; set; }

        /// <summary>
        /// Gets or sets a one-sentence statement of the defect.
        /// </summary>
        [JsonPropertyName("summary")]
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the result of a verify pass. Absent on inline-only reviews.
        /// </summary>
        [JsonPropertyName("verdict")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ReviewFindingVerdict? Verdict { get; set; }

        #endregion

    }

}
