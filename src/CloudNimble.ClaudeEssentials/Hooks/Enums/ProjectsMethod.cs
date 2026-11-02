using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the operation performed by the Projects tool.
    /// </summary>
    public enum ProjectsMethod
    {

        /// <summary>
        /// Returns information about the project.
        /// </summary>
        [JsonStringEnumMemberName("project_info")]
        Info,

        /// <summary>
        /// Reads a project document.
        /// </summary>
        [JsonStringEnumMemberName("project_read")]
        Read,

        /// <summary>
        /// Searches the project knowledge base.
        /// </summary>
        [JsonStringEnumMemberName("project_search")]
        Search,

        /// <summary>
        /// Writes a project document.
        /// </summary>
        [JsonStringEnumMemberName("project_write")]
        Write,

        /// <summary>
        /// Deletes a project document.
        /// </summary>
        [JsonStringEnumMemberName("project_delete")]
        Delete,

        /// <summary>
        /// Lists project memory files.
        /// </summary>
        [JsonStringEnumMemberName("project_memory_list")]
        MemoryList,

        /// <summary>
        /// Reads a project memory file.
        /// </summary>
        [JsonStringEnumMemberName("project_memory_read")]
        MemoryRead

    }

}
