using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{
    /// <summary>
    /// Represents the response payload returned by the Claude Code Glob tool after searching
    /// for files matching a pattern.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Glob tool performs fast file pattern matching across codebases of any size.
    /// It supports standard glob patterns like <c>"**/*.js"</c> or <c>"src/**/*.ts"</c>.
    /// This response is received in the <see cref="GlobPostToolUsePayload"/>
    /// when the <c>tool_name</c> is "Glob".
    /// </para>
    /// <para>
    /// Results are sorted by modification time (most recently modified first), making it
    /// easy to find recently changed files matching a pattern.
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "filenames": [
    ///     "C:\\Projects\\MyApp\\src\\Program.cs",
    ///     "C:\\Projects\\MyApp\\src\\Utilities.cs",
    ///     "C:\\Projects\\MyApp\\tests\\ProgramTests.cs"
    ///   ],
    ///   "durationMs": 150,
    ///   "numFiles": 3,
    ///   "truncated": false
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class GlobToolResponse
    {
        /// <summary>
        /// Gets or sets the list of absolute file paths matching the glob pattern.
        /// </summary>
        /// <remarks>
        /// <para>
        /// All paths are absolute and sorted by modification time, with the most recently
        /// modified files appearing first in the list.
        /// </para>
        /// <para>
        /// On Windows, paths will use backslash separators. The paths can be used directly
        /// with other tools like <see cref="ReadToolInput"/> to read the matched files.
        /// </para>
        /// <para>
        /// If <see cref="Truncated"/> is <c>true</c>, not all matching files are included
        /// in this list due to result limits.
        /// </para>
        /// </remarks>
        [JsonPropertyName("filenames")]
        public List<string> Filenames { get; set; } = [];

        /// <summary>
        /// Gets or sets the duration of the glob operation in milliseconds.
        /// </summary>
        /// <remarks>
        /// This indicates how long the file system search took to complete. Large codebases
        /// or complex patterns may take longer. The Glob tool is optimized for performance
        /// and typically completes quickly even on large codebases.
        /// </remarks>
        [JsonPropertyName("durationMs")]
        public int DurationMs { get; set; }

        /// <summary>
        /// Gets or sets the number of files found matching the pattern.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the count of files in <see cref="Filenames"/>. If <see cref="Truncated"/>
        /// is <c>true</c>, this represents only the number of files returned, not the total
        /// number of matches.
        /// </para>
        /// <para>
        /// A value of 0 indicates no files matched the specified pattern in the search directory.
        /// </para>
        /// </remarks>
        [JsonPropertyName("numFiles")]
        public int NumFiles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the results were truncated due to
        /// exceeding the maximum result limit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When <c>true</c>, more files matched the pattern than could be returned.
        /// Consider using a more specific pattern to narrow down the results, or use
        /// the <see cref="GlobToolInput.Path"/> parameter to search a more specific directory.
        /// </para>
        /// <para>
        /// When results are truncated, the returned files are still sorted by modification
        /// time, so the most recently modified matches are included.
        /// </para>
        /// </remarks>
        [JsonPropertyName("truncated")]
        public bool Truncated { get; set; }
    }
}
