using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the Write tool.
    /// The Write tool writes a file to the local filesystem.
    /// </summary>
    /// <remarks>
    /// This tool will overwrite the existing file if there is one at the provided path.
    /// </remarks>
    public class WriteToolInput
    {

        /// <summary>
        /// Gets or sets the absolute path to the file to write.
        /// </summary>
        /// <remarks>
        /// Must be an absolute path, not a relative path.
        /// </remarks>
        [JsonPropertyName("file_path")]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the content to write to the file.
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

    }

}
