using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a DirectoryAdded hook, which runs when a directory is added to the session.
    /// </summary>
    public class DirectoryAddedHookInput : HookInputBase
    {

        #region Properties

        /// <summary>
        /// Gets or sets the absolute path of the directory that was added.
        /// </summary>
        [JsonPropertyName("directory")]
        public string Directory { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets how the directory was added.
        /// </summary>
        [JsonPropertyName("source")]
        public DirectoryAddedSource Source { get; set; }

        #endregion

    }

}
