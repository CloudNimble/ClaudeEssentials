using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the Edit tool.
    /// The Edit tool performs exact string replacements in files.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The edit will fail if <see cref="OldString"/> is not unique in the file,
    /// unless <see cref="ReplaceAll"/> is set to <c>true</c>.
    /// </para>
    /// </remarks>
    public class EditToolInput
    {

        /// <summary>
        /// Gets or sets the absolute path to the file to modify.
        /// </summary>
        [JsonPropertyName("file_path")]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the exact text to replace.
        /// </summary>
        [JsonPropertyName("old_string")]
        public string OldString { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the text to replace it with.
        /// </summary>
        /// <remarks>
        /// Must be different from <see cref="OldString"/>.
        /// </remarks>
        [JsonPropertyName("new_string")]
        public string NewString { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether to replace all occurrences of <see cref="OldString"/>.
        /// </summary>
        /// <remarks>
        /// Default is <c>false</c>. Set to <c>true</c> to replace all occurrences,
        /// useful for renaming variables across a file.
        /// </remarks>
        [JsonPropertyName("replace_all")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? ReplaceAll { get; set; }

    }

}
