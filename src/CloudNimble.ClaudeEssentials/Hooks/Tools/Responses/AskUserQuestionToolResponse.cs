using System.Collections.Generic;
using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the Claude Code AskUserQuestion tool
    /// after the user has answered the presented questions.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The response includes the original questions, the user's answers keyed by question text,
    /// and optional per-question annotations such as preview selections or free-text notes.
    /// </para>
    /// <para>
    /// Multi-select answers are comma-separated in the <see cref="Answers"/> dictionary values.
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "questions": [ ... ],
    ///   "answers": {
    ///     "Which library should we use for date formatting?": "date-fns"
    ///   },
    ///   "annotations": {
    ///     "Which library should we use for date formatting?": {
    ///       "preview": "import { format } from 'date-fns';",
    ///       "notes": "I prefer this because it's tree-shakeable"
    ///     }
    ///   }
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class AskUserQuestionToolResponse
    {

        /// <summary>
        /// Gets or sets the questions that were asked.
        /// </summary>
        [JsonPropertyName("questions")]
        public List<AskUserQuestion> Questions { get; set; } = [];

        /// <summary>
        /// Gets or sets the answers provided by the user.
        /// Keyed by question text; multi-select answers are comma-separated.
        /// </summary>
        [JsonPropertyName("answers")]
        public Dictionary<string, string> Answers { get; set; } = [];

        /// <summary>
        /// Gets or sets optional per-question annotations from the user (e.g., notes on preview selections).
        /// Keyed by question text.
        /// </summary>
        [JsonPropertyName("annotations")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, AskUserQuestionAnnotation>? Annotations { get; set; }

    }

}
