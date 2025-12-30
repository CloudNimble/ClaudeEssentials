using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{
    /// <summary>
    /// Represents the response payload returned by the Claude Code Read tool after reading a file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Read tool reads files from the local filesystem and returns their content along with
    /// metadata about the file. This response is received in the <see cref="ReadPostToolUsePayload"/>
    /// when the <c>tool_name</c> is "Read".
    /// </para>
    /// <para>
    /// The Read tool supports reading text files, images (PNG, JPG, etc.), PDF files, and Jupyter notebooks (.ipynb).
    /// For text files, content is returned with line numbers. For binary files like images, the content
    /// is presented visually to Claude as it is a multimodal LLM.
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "type": "text",
    ///   "file": {
    ///     "filePath": "C:\\Projects\\MyApp\\Program.cs",
    ///     "content": "     1→using System;\n     2→\n     3→class Program...",
    ///     "numLines": 50,
    ///     "startLine": 1,
    ///     "totalLines": 50
    ///   }
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class ReadToolResponse
    {
        /// <summary>
        /// Gets or sets the type of content returned by the Read tool.
        /// </summary>
        /// <remarks>
        /// Common values include:
        /// <list type="bullet">
        /// <item><description><c>"text"</c> - Standard text file content</description></item>
        /// <item><description><c>"image"</c> - Binary image data (PNG, JPG, etc.)</description></item>
        /// <item><description><c>"pdf"</c> - PDF document content</description></item>
        /// <item><description><c>"notebook"</c> - Jupyter notebook content</description></item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file information containing the path, content, and line metadata.
        /// </summary>
        /// <remarks>
        /// This property contains the actual file content and associated metadata such as
        /// the number of lines read, the starting line, and the total lines in the file.
        /// May be <c>null</c> if the file could not be read or does not exist.
        /// </remarks>
        [JsonPropertyName("file")]
        public ReadToolFileInfo? File { get; set; }
    }

    /// <summary>
    /// Contains detailed information about a file read by the Claude Code Read tool,
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
        /// <example>
        /// <code>
        /// "     1→using System;\n     2→using System.Text.Json;\n     3→\n     4→namespace MyApp"
        /// </code>
        /// </example>
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
    }
}
