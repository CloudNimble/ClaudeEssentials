using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the SendFeedback tool, which drafts feedback about Claude Code.
    /// </summary>
    public class SendFeedbackToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets an optional short tag naming the part of Claude Code this is about.
        /// </summary>
        [JsonPropertyName("area")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Area { get; set; }

        /// <summary>
        /// Gets or sets the labeled details of the issue.
        /// </summary>
        [JsonPropertyName("details")]
        public string Details { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the closest failure mode when the report is about model behavior.
        /// </summary>
        [JsonPropertyName("failure_mode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public FeedbackFailureMode? FailureMode { get; set; }

        /// <summary>
        /// Gets or sets what kind of task the session was doing when the issue occurred.
        /// </summary>
        [JsonPropertyName("task_category")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public FeedbackTaskCategory? TaskCategory { get; set; }

        /// <summary>
        /// Gets or sets a short, specific one-line summary of the issue.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets what kind of feedback this is.
        /// </summary>
        [JsonPropertyName("type")]
        public FeedbackType Type { get; set; }

        #endregion

    }

}
