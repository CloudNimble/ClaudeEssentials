using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents a single option choice within an <see cref="AskUserQuestion"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each option is a distinct choice presented to the user. Options should be concise
    /// (1-5 words for the label) with a description explaining what the option means or
    /// what will happen if chosen.
    /// </para>
    /// <para>
    /// Example JSON:
    /// <code>
    /// {
    ///   "label": "Use Tailwind",
    ///   "description": "Style with utility classes using Tailwind CSS framework",
    ///   "preview": "/* Tailwind example */\n.btn { @apply px-4 py-2 bg-blue-500; }"
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class AskUserQuestionOption
    {

        /// <summary>
        /// Gets or sets the display text for this option that the user will see and select.
        /// Should be concise (1-5 words) and clearly describe the choice.
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the explanation of what this option means or what will happen if chosen.
        /// Useful for providing context about trade-offs or implications.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets optional preview content rendered when this option is focused.
        /// Use for mockups, code snippets, or visual comparisons that help users compare options.
        /// </summary>
        [JsonPropertyName("preview")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Preview { get; set; }

    }

}
