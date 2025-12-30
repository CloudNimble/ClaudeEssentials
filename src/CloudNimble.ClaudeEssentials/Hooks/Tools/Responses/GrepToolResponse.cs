using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{
    /// <summary>
    /// Represents the response payload returned by the Claude Code Grep tool after searching
    /// for content within files.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Grep tool is built on ripgrep and provides powerful content search capabilities
    /// with full regex support. This response is received in the
    /// <see cref="GrepPostToolUsePayload"/> when the
    /// <c>tool_name</c> is "Grep".
    /// </para>
    /// <para>
    /// The response structure varies based on the <see cref="GrepToolInput.OutputMode"/>:
    /// <list type="bullet">
    /// <item><description><c>"files_with_matches"</c> (default) - Returns only file paths containing matches</description></item>
    /// <item><description><c>"content"</c> - Returns matching lines with context</description></item>
    /// <item><description><c>"count"</c> - Returns match counts per file</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Example JSON payload (files_with_matches mode):
    /// <code>
    /// {
    ///   "mode": "files_with_matches",
    ///   "filenames": [
    ///     "C:\\Projects\\MyApp\\src\\Program.cs",
    ///     "C:\\Projects\\MyApp\\src\\Utilities.cs"
    ///   ],
    ///   "numFiles": 2
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class GrepToolResponse
    {
        /// <summary>
        /// Gets or sets the output mode that was used for the search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This indicates which output format was requested and determines which properties
        /// contain the results:
        /// <list type="bullet">
        /// <item><description><c>"files_with_matches"</c> - Results are in <see cref="Filenames"/></description></item>
        /// <item><description><c>"content"</c> - Results are in <see cref="Content"/></description></item>
        /// <item><description><c>"count"</c> - Results are in <see cref="Counts"/></description></item>
        /// </list>
        /// </para>
        /// </remarks>
        [JsonPropertyName("mode")]
        public string Mode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of file paths containing matches.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property is populated when <see cref="Mode"/> is <c>"files_with_matches"</c>.
        /// It contains the absolute paths to all files where the search pattern was found.
        /// </para>
        /// <para>
        /// Files are listed in the order they were found. Use the
        /// <see cref="GrepToolInput.HeadLimit"/> parameter to limit the number of results.
        /// </para>
        /// </remarks>
        [JsonPropertyName("filenames")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Filenames { get; set; }

        /// <summary>
        /// Gets or sets the number of files that contained matches.
        /// </summary>
        /// <remarks>
        /// This represents the count of unique files where the pattern was found.
        /// When <see cref="Mode"/> is <c>"files_with_matches"</c>, this equals the
        /// length of <see cref="Filenames"/>.
        /// </remarks>
        [JsonPropertyName("numFiles")]
        public int NumFiles { get; set; }

        /// <summary>
        /// Gets or sets the matching content with line numbers and optional context.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property is populated when <see cref="Mode"/> is <c>"content"</c>.
        /// It contains the actual matching lines from the files, formatted with line numbers.
        /// </para>
        /// <para>
        /// The format includes the file path, line number, and matching content.
        /// Additional context lines can be included using the <c>-A</c>, <c>-B</c>, or <c>-C</c>
        /// parameters in <see cref="GrepToolInput"/>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// "C:\\Projects\\MyApp\\src\\Program.cs:10:    public static void Main(string[] args)\nC:\\Projects\\MyApp\\src\\Program.cs:15:    static void Helper()"
        /// </code>
        /// </example>
        [JsonPropertyName("content")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Content { get; set; }

        /// <summary>
        /// Gets or sets the match counts per file.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property is populated when <see cref="Mode"/> is <c>"count"</c>.
        /// The dictionary keys are absolute file paths, and the values are the number
        /// of matches found in each file.
        /// </para>
        /// <para>
        /// This is useful for understanding the distribution of matches across files
        /// before deciding which files to read or edit.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// {
        ///   "C:\\Projects\\MyApp\\src\\Program.cs": 5,
        ///   "C:\\Projects\\MyApp\\src\\Utilities.cs": 2
        /// }
        /// </code>
        /// </example>
        [JsonPropertyName("counts")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, int>? Counts { get; set; }
    }
}
