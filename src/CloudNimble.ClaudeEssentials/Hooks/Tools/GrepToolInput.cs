using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the input parameters for the Grep tool.
    /// The Grep tool is a powerful content search built on ripgrep.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Supports full regex syntax (e.g., "log.*Error", "function\s+\w+").
    /// Filter files with glob parameter or type parameter.
    /// </para>
    /// </remarks>
    public class GrepToolInput
    {

        /// <summary>
        /// Gets or sets the regular expression pattern to search for in file contents.
        /// </summary>
        /// <remarks>
        /// Uses ripgrep syntax. Literal braces need escaping (use "interface\{\}" to find "interface{}" in Go code).
        /// </remarks>
        [JsonPropertyName("pattern")]
        public string Pattern { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file or directory to search in.
        /// </summary>
        /// <remarks>
        /// Defaults to current working directory if not specified.
        /// </remarks>
        [JsonPropertyName("path")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the output mode.
        /// </summary>
        /// <remarks>
        /// Options: "content" (shows matching lines), "files_with_matches" (shows only file paths, default), "count" (shows match counts).
        /// </remarks>
        [JsonPropertyName("output_mode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OutputMode { get; set; }

        /// <summary>
        /// Gets or sets the glob pattern to filter files.
        /// </summary>
        /// <remarks>
        /// Examples: "*.js", "*.{ts,tsx}"
        /// </remarks>
        [JsonPropertyName("glob")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Glob { get; set; }

        /// <summary>
        /// Gets or sets the file type to search.
        /// </summary>
        /// <remarks>
        /// Common types: js, py, rust, go, java, etc. More efficient than glob for standard file types.
        /// </remarks>
        [JsonPropertyName("type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the number of lines to show after each match.
        /// </summary>
        /// <remarks>
        /// Requires output_mode: "content", ignored otherwise.
        /// </remarks>
        [JsonPropertyName("-A")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? LinesAfter { get; set; }

        /// <summary>
        /// Gets or sets the number of lines to show before each match.
        /// </summary>
        /// <remarks>
        /// Requires output_mode: "content", ignored otherwise.
        /// </remarks>
        [JsonPropertyName("-B")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? LinesBefore { get; set; }

        /// <summary>
        /// Gets or sets the number of lines to show before and after each match.
        /// </summary>
        /// <remarks>
        /// Requires output_mode: "content", ignored otherwise.
        /// </remarks>
        [JsonPropertyName("-C")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? LinesContext { get; set; }

        /// <summary>
        /// Gets or sets whether to perform case insensitive search.
        /// </summary>
        [JsonPropertyName("-i")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? CaseInsensitive { get; set; }

        /// <summary>
        /// Gets or sets whether to show line numbers in output.
        /// </summary>
        /// <remarks>
        /// Requires output_mode: "content", ignored otherwise. Defaults to true.
        /// </remarks>
        [JsonPropertyName("-n")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? ShowLineNumbers { get; set; }

        /// <summary>
        /// Gets or sets whether to enable multiline mode.
        /// </summary>
        /// <remarks>
        /// When enabled, '.' matches newlines and patterns can span lines. Default: false.
        /// </remarks>
        [JsonPropertyName("multiline")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Multiline { get; set; }

        /// <summary>
        /// Gets or sets the limit on output to first N lines/entries.
        /// </summary>
        /// <remarks>
        /// Equivalent to "| head -N". Defaults to 0 (unlimited).
        /// </remarks>
        [JsonPropertyName("head_limit")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? HeadLimit { get; set; }

        /// <summary>
        /// Gets or sets the number of lines/entries to skip before applying head_limit.
        /// </summary>
        /// <remarks>
        /// Equivalent to "| tail -n +N | head -N". Defaults to 0.
        /// </remarks>
        [JsonPropertyName("offset")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Offset { get; set; }

    }

}
