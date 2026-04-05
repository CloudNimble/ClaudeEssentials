using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the type of a cell within a Jupyter notebook.
    /// </summary>
    public enum NotebookCellType
    {

        /// <summary>
        /// A cell containing executable code.
        /// </summary>
        [JsonStringEnumMemberName("code")]
        Code,

        /// <summary>
        /// A cell containing Markdown-formatted text.
        /// </summary>
        [JsonStringEnumMemberName("markdown")]
        Markdown

    }

}
