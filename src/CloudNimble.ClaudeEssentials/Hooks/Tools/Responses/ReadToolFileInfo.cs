using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Contains detailed information about a text file read by the Claude Code Read tool,
    /// including its path, content, and line-related metadata.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The content returned by the Read tool includes line numbers in a specific format:
    /// spaces followed by the line number, a tab character, and then the actual content.
    /// For example: <c>"     1→using System;"</c>
    /// </para>
    /// <para>
    /// When using the <c>offset</c> and <c>limit</c> parameters in <see cref="ReadToolInput"/>,
    /// the <see cref="StartLine"/> and <see cref="NumLines"/> properties will reflect the
    /// subset of lines that were actually returned, while <see cref="TotalLines"/> always
    /// represents the total number of lines in the entire file.
    /// </para>
    /// </remarks>
    public class ReadToolFileInfo
    {

        /// <summary>
        /// Gets or sets the absolute path to the file that was read.
        /// </summary>
        /// <remarks>
        /// This is always an absolute path, regardless of whether the original request
        /// used a relative or absolute path. On Windows, this will use backslash separators.
        /// </remarks>
        /// <example>
        /// <code>
        /// "C:\\Users\\Developer\\Projects\\MyApp\\src\\Program.cs"
        /// </code>
        /// </example>
        [JsonPropertyName("filePath")]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the content of the file, formatted with line numbers.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The content is formatted using <c>cat -n</c> style output, where each line is prefixed
        /// with its line number. The format is: spaces for padding, line number, tab character (→),
        /// then the actual line content.
        /// </para>
        /// <para>
        /// Lines longer than 2000 characters are automatically truncated by Claude Code.
        /// </para>
        /// </remarks>
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the number of lines included in this response.
        /// </summary>
        /// <remarks>
        /// This value reflects the actual number of lines returned, which may be less than
        /// <see cref="TotalLines"/> if the <c>offset</c> and <c>limit</c> parameters were used
        /// in the original <see cref="ReadToolInput"/>, or if the file was truncated due to size.
        /// By default, the Read tool returns up to 2000 lines.
        /// </remarks>
        [JsonPropertyName("numLines")]
        public int NumLines { get; set; }

        /// <summary>
        /// Gets or sets the starting line number (1-based) of the content in this response.
        /// </summary>
        /// <remarks>
        /// Line numbers in Claude Code are 1-based, meaning the first line of a file is line 1.
        /// If an <c>offset</c> was specified in the <see cref="ReadToolInput"/>, this value
        /// will reflect that offset.
        /// </remarks>
        [JsonPropertyName("startLine")]
        public int StartLine { get; set; }

        /// <summary>
        /// Gets or sets the total number of lines in the entire file.
        /// </summary>
        /// <remarks>
        /// This represents the complete line count of the file, regardless of how many lines
        /// were actually returned in this response. Use this to determine if the file was
        /// partially read (when <see cref="NumLines"/> is less than <see cref="TotalLines"/>).
        /// </remarks>
        [JsonPropertyName("totalLines")]
        public int TotalLines { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a whole-file read was auto-paginated because it exceeded the token cap.
        /// </summary>
        [JsonPropertyName("truncatedByTokenCap")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? TruncatedByTokenCap { get; set; }

    }

}
