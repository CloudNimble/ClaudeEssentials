using System.Text.Json.Serialization;
namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a FileChanged hook.
    /// This hook runs when a file is changed externally.
    /// </summary>
    public class FileChangedHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the path of the file that changed.
        /// </summary>
        [JsonPropertyName("file_path")]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the kind of change.
        /// </summary>
        [JsonPropertyName("event")]
        public FileChangeEvent Event { get; set; }

    }

}
