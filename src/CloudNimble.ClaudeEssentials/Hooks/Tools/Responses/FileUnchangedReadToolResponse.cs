using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Read tool response when a file has not changed since it was last read.
    /// </summary>
    public class FileUnchangedReadToolResponse : ReadToolResponse
    {

        /// <summary>
        /// Gets or sets the file information containing only the file path.
        /// </summary>
        [JsonPropertyName("file")]
        public FileUnchangedReadToolFileInfo File { get; set; } = new();

    }

}
