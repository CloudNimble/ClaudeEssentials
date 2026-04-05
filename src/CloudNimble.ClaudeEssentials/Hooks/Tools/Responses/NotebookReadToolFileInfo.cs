using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Contains information about a Jupyter notebook file read by the Claude Code Read tool.
    /// </summary>
    public class NotebookReadToolFileInfo
    {

        /// <summary>
        /// Gets or sets the path to the notebook file.
        /// </summary>
        [JsonPropertyName("filePath")]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the array of notebook cells.
        /// </summary>
        [JsonPropertyName("cells")]
        public List<JsonElement> Cells { get; set; } = [];

    }

}
