using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the AskUserQuestion tool.
    /// The AskUserQuestion tool presents structured multiple-choice questions to the user.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The tool supports 1-4 questions, each with 2-4 options. An "Other" option is provided
    /// automatically and should not be included in the options list.
    /// </para>
    /// <para>
    /// Example JSON:
    /// <code>
    /// {
    ///   "questions": [
    ///     {
    ///       "question": "Which library should we use for date formatting?",
    ///       "header": "Library",
    ///       "options": [
    ///         { "label": "date-fns", "description": "Lightweight, tree-shakeable" },
    ///         { "label": "Day.js", "description": "2KB Moment.js alternative" }
    ///       ],
    ///       "multiSelect": false
    ///     }
    ///   ]
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class AskUserQuestionToolInput
    {

        /// <summary>
        /// Gets or sets the questions to ask the user (1-4 questions).
        /// </summary>
        [JsonPropertyName("questions")]
        public List<AskUserQuestion> Questions { get; set; } = [];

    }

}
