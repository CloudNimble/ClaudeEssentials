using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Contains information about a file that was unchanged since it was last read.
    /// </summary>
    public class FileUnchangedReadToolFileInfo
    {

        /// <summary>
        /// Gets or sets the path to the file.
        /// </summary>
        [JsonPropertyName("filePath")]
        public string FilePath { get; set; } = string.Empty;

    }

}
