using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the ReadMcpResourceDir tool.
    /// </summary>
    public class ReadMcpResourceDirToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets a human-readable error when the server could not list the directory.
        /// </summary>
        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; }

        /// <summary>
        /// Gets or sets the direct children of the directory resource. Subdirectories have the MIME type <c>inode/directory</c>.
        /// </summary>
        [JsonPropertyName("resources")]
        public List<McpDirectoryResourceInfo> Resources { get; set; } = [];

        #endregion

    }

}
