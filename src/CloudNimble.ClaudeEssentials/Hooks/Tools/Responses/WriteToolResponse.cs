using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{
    /// <summary>
    /// Represents the response payload returned by the Claude Code Write tool after creating or overwriting a file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Write tool creates new files or overwrites existing files on the local filesystem.
    /// This response is received in the <see cref="WritePostToolUsePayload"/>
    /// when the <c>tool_name</c> is "Write".
    /// </para>
    /// <para>
    /// Unlike the Edit tool which performs targeted string replacements, the Write tool completely
    /// replaces the file content. Claude Code requires that an existing file be read first before
    /// it can be overwritten, to prevent accidental data loss.
    /// </para>
    /// <para>
    /// Example JSON payload for a new file:
    /// <code>
    /// {
    ///   "type": "create",
    ///   "filePath": "C:\\Projects\\MyApp\\NewFile.cs",
    ///   "content": "using System;\n\nnamespace MyApp { }",
    ///   "structuredPatch": [],
    ///   "originalFile": null
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class WriteToolResponse
    {
        /// <summary>
        /// Gets or sets the type of write operation that was performed.
        /// </summary>
        /// <remarks>
        /// Known values include:
        /// <list type="bullet">
        /// <item><description><c>"create"</c> - A new file was created (file did not exist before)</description></item>
        /// <item><description><c>"overwrite"</c> - An existing file was completely overwritten</description></item>
        /// </list>
        /// </remarks>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the absolute path to the file that was written.
        /// </summary>
        /// <remarks>
        /// This is always an absolute path, regardless of whether the original request
        /// used a relative or absolute path. On Windows, this will use backslash separators.
        /// </remarks>
        /// <example>
        /// <code>
        /// "C:\\Users\\Developer\\Projects\\MyApp\\src\\NewClass.cs"
        /// </code>
        /// </example>
        [JsonPropertyName("filePath")]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the content that was written to the file.
        /// </summary>
        /// <remarks>
        /// This contains the complete content of the file after the write operation.
        /// For new files, this is the entire file content. For overwrites, this is
        /// the new content that replaced the original.
        /// </remarks>
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the structured patch representing the changes made to the file.
        /// </summary>
        /// <remarks>
        /// <para>
        /// For new files (<see cref="Type"/> = "create"), this will be an empty list since
        /// there is no previous content to diff against.
        /// </para>
        /// <para>
        /// For overwrites, this contains a unified diff representation showing what changed
        /// between the original and new content. Each <see cref="StructuredPatchHunk"/>
        /// represents a contiguous block of changes.
        /// </para>
        /// </remarks>
        [JsonPropertyName("structuredPatch")]
        public List<StructuredPatchHunk> StructuredPatch { get; set; } = [];

        /// <summary>
        /// Gets or sets the original file content before the write operation.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is <c>null</c> when creating a new file (<see cref="Type"/> = "create")
        /// since there was no previous content.
        /// </para>
        /// <para>
        /// For overwrites, this contains the complete original content of the file
        /// before it was replaced, which can be useful for implementing undo functionality
        /// or auditing changes.
        /// </para>
        /// </remarks>
        [JsonPropertyName("originalFile")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OriginalFile { get; set; }
    }
}
