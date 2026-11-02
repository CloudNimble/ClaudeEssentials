using System.Text.Json.Serialization;
using System;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{
    /// <summary>
    /// Represents the response payload returned by the Claude Code NotebookEdit tool after
    /// modifying a Jupyter notebook cell.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The NotebookEdit tool modifies Jupyter notebook (.ipynb) files by replacing, inserting,
    /// or deleting cells. Jupyter notebooks are interactive documents that combine code, text,
    /// and visualizations, commonly used for data analysis and scientific computing.
    /// This response is received in the <see cref="NotebookEditPostToolUsePayload"/>
    /// when the <c>tool_name</c> is "NotebookEdit".
    /// </para>
    /// <para>
    /// Supported edit operations:
    /// <list type="bullet">
    /// <item><description><c>replace</c> - Replaces the content of an existing cell</description></item>
    /// <item><description><c>insert</c> - Adds a new cell at a specified position</description></item>
    /// <item><description><c>delete</c> - Removes a cell from the notebook</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "notebook_path": "C:\\Projects\\Analysis\\data_exploration.ipynb",
    ///   "cell_id": "cell_abc123",
    ///   "edit_mode": "replace"
    /// }
    /// </code>
    /// </para>
    /// <para>
    /// Unlike most other tool responses which use camelCase property names, NotebookEdit uses
    /// snake_case for all properties in both its input and output payloads.
    /// </para>
    /// </remarks>
    public class NotebookEditToolResponse
    {
        /// <summary>
        /// Gets or sets the absolute path to the Jupyter notebook that was edited.
        /// </summary>
        /// <remarks>
        /// This is always an absolute path, regardless of whether the original request
        /// used a relative or absolute path. The file extension will be <c>.ipynb</c>.
        /// </remarks>
        /// <example>
        /// <code>
        /// "C:\\Users\\Developer\\Projects\\Analysis\\data_exploration.ipynb"
        /// </code>
        /// </example>
        [JsonPropertyName("notebook_path")]
        public string NotebookPath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identifier of the cell that was edited, inserted, or deleted.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Cell IDs are unique identifiers within a notebook that persist across edits.
        /// For insert operations, this is the ID of the newly created cell.
        /// For delete operations, this is the ID of the cell that was removed.
        /// </para>
        /// <para>
        /// May be <c>null</c> if the operation did not involve a specific cell or if
        /// the cell was identified by index rather than ID.
        /// </para>
        /// </remarks>
        [JsonPropertyName("cell_id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CellId { get; set; }

        /// <summary>
        /// Gets or sets the type of edit operation that was performed.
        /// </summary>
        /// <remarks>
        /// <para>
        /// See <see cref="NotebookEditMode"/> for possible values.
        /// </para>
        /// <para>
        /// This should match the <see cref="NotebookEditToolInput.EditMode"/> that was
        /// specified in the input, defaulting to <see cref="NotebookEditMode.Replace"/> if not specified.
        /// </para>
        /// </remarks>
        [JsonPropertyName("edit_mode")]
        public NotebookEditMode EditMode { get; set; }

        /// <summary>
        /// Gets or sets the new source code that was written to the cell.
        /// </summary>
        [JsonPropertyName("new_source")]
        public string NewSource { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the cell.
        /// </summary>
        /// <remarks>
        /// See <see cref="NotebookCellType"/> for possible values.
        /// </remarks>
        [JsonPropertyName("cell_type")]
        public NotebookCellType CellType { get; set; }

        /// <summary>
        /// Gets or sets the programming language of the notebook.
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the error message if the operation failed.
        /// </summary>
        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; }

        /// <summary>
        /// Gets or sets the original notebook content before modification.
        /// </summary>
        [JsonPropertyName("original_file")]
        public string OriginalFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the updated notebook content after modification.
        /// </summary>
        [JsonPropertyName("updated_file")]
        public string UpdatedFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the previous cell source. Applies to replace and delete operations only.
        /// </summary>
        [JsonPropertyName("old_source")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OldSource { get; set; }
    }
}
