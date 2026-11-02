using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Converters;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents per-question annotations from the user in an AskUserQuestion response,
    /// such as notes on preview selections.
    /// </summary>
    public class AskUserQuestionAnnotation
    {

        /// <summary>
        /// Gets or sets the preview content of the selected option, if the question used previews.
        /// </summary>
        [JsonPropertyName("preview")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Preview { get; set; }

        /// <summary>
        /// Gets or sets free-text notes the user added to their selection.
        /// </summary>
        [JsonPropertyName("notes")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets how the question went unanswered. Absent when the question was answered, and unknown values are treated as absent.
        /// </summary>
        [JsonPropertyName("unanswered")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(AskUserQuestionUnansweredReasonConverter))]
        public AskUserQuestionUnansweredReason? Unanswered { get; set; }

    }

}
