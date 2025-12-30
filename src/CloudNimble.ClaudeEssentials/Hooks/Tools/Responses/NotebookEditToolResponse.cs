using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using System.Text.Json.Serialization;

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
    ///   "notebookPath": "C:\\Projects\\Analysis\\data_exploration.ipynb",
    ///   "cellId": "cell_abc123",
    ///   "editMode": "replace",
    ///   "success": true
    /// }
    /// </code>
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
        [JsonPropertyName("notebookPath")]
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
        [JsonPropertyName("cellId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CellId { get; set; }

        /// <summary>
        /// Gets or sets the type of edit operation that was performed.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Indicates which operation was executed:
        /// <list type="bullet">
        /// <item><description><c>"replace"</c> - The cell's source content was replaced</description></item>
        /// <item><description><c>"insert"</c> - A new cell was added to the notebook</description></item>
        /// <item><description><c>"delete"</c> - A cell was removed from the notebook</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// This should match the <see cref="NotebookEditToolInput.EditMode"/> that was
        /// specified in the input, defaulting to <c>"replace"</c> if not specified.
        /// </para>
        /// </remarks>
        [JsonPropertyName("editMode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? EditMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the notebook edit was successful.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When <c>true</c>, the notebook was successfully modified and saved.
        /// </para>
        /// <para>
        /// When <c>false</c>, the edit operation failed. Common failure reasons include:
        /// <list type="bullet">
        /// <item><description>The specified cell ID or index does not exist</description></item>
        /// <item><description>The notebook file could not be written (permissions, disk space)</description></item>
        /// <item><description>The notebook format is invalid or corrupted</description></item>
        /// <item><description>Required parameters were missing or invalid</description></item>
        /// </list>
        /// </para>
        /// </remarks>
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
