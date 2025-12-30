using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the NotebookEdit tool.
    /// The NotebookEdit tool replaces the contents of a specific cell in a Jupyter notebook.
    /// </summary>
    public class NotebookEditToolInput
    {

        /// <summary>
        /// Gets or sets the absolute path to the Jupyter notebook file to edit.
        /// </summary>
        /// <remarks>
        /// Must be an absolute path, not a relative path.
        /// </remarks>
        [JsonPropertyName("notebook_path")]
        public string NotebookPath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new source for the cell.
        /// </summary>
        [JsonPropertyName("new_source")]
        public string NewSource { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ID of the cell to edit.
        /// </summary>
        /// <remarks>
        /// When inserting a new cell, the new cell will be inserted after the cell with this ID,
        /// or at the beginning if not specified.
        /// </remarks>
        [JsonPropertyName("cell_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CellId { get; set; }

        /// <summary>
        /// Gets or sets the type of the cell.
        /// </summary>
        /// <remarks>
        /// Options: "code" or "markdown". If not specified, defaults to the current cell type.
        /// Required when using edit_mode=insert.
        /// </remarks>
        [JsonPropertyName("cell_type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CellType { get; set; }

        /// <summary>
        /// Gets or sets the type of edit to make.
        /// </summary>
        /// <remarks>
        /// Options: "replace" (default), "insert", or "delete".
        /// </remarks>
        [JsonPropertyName("edit_mode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? EditMode { get; set; }

    }

}
