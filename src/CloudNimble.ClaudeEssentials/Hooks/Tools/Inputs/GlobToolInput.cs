using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the Glob tool.
    /// The Glob tool provides fast file pattern matching that works with any codebase size.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Supports glob patterns like "**/*.js" or "src/**/*.ts".
    /// Returns matching file paths sorted by modification time.
    /// </para>
    /// </remarks>
    public class GlobToolInput
    {

        /// <summary>
        /// Gets or sets the glob pattern to match files against.
        /// </summary>
        /// <remarks>
        /// Examples: "**/*.js", "src/**/*.ts", "*.md"
        /// </remarks>
        [JsonPropertyName("pattern")]
        public string Pattern { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the directory to search in.
        /// </summary>
        /// <remarks>
        /// If not specified, the current working directory will be used.
        /// Must be a valid directory path if provided.
        /// </remarks>
        [JsonPropertyName("path")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Path { get; set; }

    }

}
