using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents a single question within the AskUserQuestion tool input.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each question includes a prompt, a short header label, a set of options to choose from,
    /// and whether multiple selections are allowed. Questions should be clear, specific, and
    /// end with a question mark.
    /// </para>
    /// <para>
    /// Example JSON:
    /// <code>
    /// {
    ///   "question": "Which library should we use for date formatting?",
    ///   "header": "Library",
    ///   "options": [
    ///     { "label": "date-fns", "description": "Lightweight, tree-shakeable date utility library" },
    ///     { "label": "Day.js", "description": "2KB immutable date library, Moment.js alternative" }
    ///   ],
    ///   "multiSelect": false
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class AskUserQuestion
    {

        /// <summary>
        /// Gets or sets the complete question to ask the user. Should be clear, specific, and end with a question mark.
        /// If <see cref="MultiSelect"/> is <c>true</c>, phrase it accordingly, e.g. "Which features do you want to enable?"
        /// </summary>
        [JsonPropertyName("question")]
        public string Question { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a very short label displayed as a chip/tag (max 12 chars).
        /// Examples: "Auth method", "Library", "Approach".
        /// </summary>
        [JsonPropertyName("header")]
        public string Header { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the available choices for this question. Must have 2-4 options.
        /// Each option should be a distinct, mutually exclusive choice (unless <see cref="MultiSelect"/> is enabled).
        /// There should be no 'Other' option, that will be provided automatically.
        /// </summary>
        [JsonPropertyName("options")]
        public List<AskUserQuestionOption> Options { get; set; } = [];

        /// <summary>
        /// Gets or sets whether to allow the user to select multiple options instead of just one.
        /// Use when choices are not mutually exclusive.
        /// </summary>
        [JsonPropertyName("multiSelect")]
        public bool MultiSelect { get; set; }

    }

}
