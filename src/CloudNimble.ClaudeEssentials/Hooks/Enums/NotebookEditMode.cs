using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the edit operation mode applied to a Jupyter notebook cell.
    /// </summary>
    public enum NotebookEditMode
    {

        /// <summary>
        /// Replaces the content of an existing cell.
        /// </summary>
        [JsonStringEnumMemberName("replace")]
        Replace,

        /// <summary>
        /// Inserts a new cell at the specified position.
        /// </summary>
        [JsonStringEnumMemberName("insert")]
        Insert,

        /// <summary>
        /// Deletes the cell at the specified position.
        /// </summary>
        [JsonStringEnumMemberName("delete")]
        Delete

    }

}
