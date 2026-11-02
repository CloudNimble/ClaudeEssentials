using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Projects tool response for the <c>project_memory_list</c> method.
    /// </summary>
    public class MemoryListProjectsToolResponse : ProjectsToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the project memory files.
        /// </summary>
        [JsonPropertyName("files")]
        public List<ProjectMemoryFileInfo> Files { get; set; } = [];

        /// <summary>
        /// Gets or sets a value indicating whether the list was truncated.
        /// </summary>
        [JsonPropertyName("truncated")]
        public bool Truncated { get; set; }

        #endregion

    }

}
