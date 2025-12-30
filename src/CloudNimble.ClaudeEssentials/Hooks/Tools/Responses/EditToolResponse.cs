using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{
    /// <summary>
    /// Represents the response payload returned by the Claude Code Edit tool after performing
    /// a string replacement in a file.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Edit tool performs exact string replacements in files. It requires that the target
    /// file has been read at least once in the conversation before editing. This response is
    /// received in the <see cref="EditPostToolUsePayload"/>
    /// when the <c>tool_name</c> is "Edit".
    /// </para>
    /// <para>
    /// The Edit tool will fail if the <see cref="EditToolInput.OldString"/> is not unique in
    /// the file, unless <see cref="EditToolInput.ReplaceAll"/> is set to <c>true</c>.
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "filePath": "C:\\Projects\\MyApp\\Program.cs",
    ///   "oldString": "Console.WriteLine(\"Hello\");",
    ///   "newString": "Console.WriteLine(\"Hello, World!\");",
    ///   "originalFile": "using System;\n\nclass Program...",
    ///   "structuredPatch": [
    ///     {
    ///       "oldStart": 5,
    ///       "oldLines": 1,
    ///       "newStart": 5,
    ///       "newLines": 1,
    ///       "lines": ["-Console.WriteLine(\"Hello\");", "+Console.WriteLine(\"Hello, World!\");"]
    ///     }
    ///   ],
    ///   "userModified": false,
    ///   "replaceAll": false
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class EditToolResponse
    {
        /// <summary>
        /// Gets or sets the absolute path to the file that was edited.
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
        /// Gets or sets the original string that was searched for and replaced.
        /// </summary>
        /// <remarks>
        /// This is the exact string that was matched in the file. The Edit tool preserves
        /// the exact indentation (tabs/spaces) from the original file content.
        /// </remarks>
        [JsonPropertyName("oldString")]
        public string OldString { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the new string that replaced the old string.
        /// </summary>
        /// <remarks>
        /// This is the replacement text that now appears in the file where
        /// <see cref="OldString"/> was previously located.
        /// </remarks>
        [JsonPropertyName("newString")]
        public string NewString { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the complete original content of the file before the edit was applied.
        /// </summary>
        /// <remarks>
        /// This contains the full file content prior to the replacement, which can be useful
        /// for implementing undo functionality, auditing changes, or verifying the context
        /// of the edit.
        /// </remarks>
        [JsonPropertyName("originalFile")]
        public string OriginalFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the structured patch representing the changes made to the file.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This contains a unified diff representation showing what changed between the
        /// original and edited content. Each <see cref="StructuredPatchHunk"/> represents
        /// a contiguous block of changes.
        /// </para>
        /// <para>
        /// When <see cref="ReplaceAll"/> is <c>true</c> and multiple replacements were made,
        /// there may be multiple hunks in the patch, one for each replacement location.
        /// </para>
        /// </remarks>
        [JsonPropertyName("structuredPatch")]
        public List<StructuredPatchHunk> StructuredPatch { get; set; } = [];

        /// <summary>
        /// Gets or sets a value indicating whether the file was modified by the user
        /// since it was last read by Claude.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If <c>true</c>, Claude detected that the file content changed between when it
        /// was read and when the edit was applied. This can happen if the user edits the
        /// file externally while Claude is working.
        /// </para>
        /// <para>
        /// This flag helps track potential merge conflicts or unexpected changes in the
        /// editing workflow.
        /// </para>
        /// </remarks>
        [JsonPropertyName("userModified")]
        public bool UserModified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether all occurrences of the old string
        /// were replaced, or just the first occurrence.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When <c>false</c> (the default), the Edit tool requires that <see cref="OldString"/>
        /// appears exactly once in the file, and only that single occurrence is replaced.
        /// </para>
        /// <para>
        /// When <c>true</c>, all occurrences of <see cref="OldString"/> in the file are
        /// replaced with <see cref="NewString"/>. This is useful for renaming variables
        /// or updating repeated patterns throughout a file.
        /// </para>
        /// </remarks>
        [JsonPropertyName("replaceAll")]
        public bool ReplaceAll { get; set; }
    }

    /// <summary>
    /// Represents a single hunk (contiguous block of changes) in a unified diff patch.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A structured patch hunk follows the unified diff format, showing context lines
    /// and changed lines together. Each hunk represents changes to a contiguous section
    /// of the file.
    /// </para>
    /// <para>
    /// The <see cref="Lines"/> collection uses standard diff prefixes:
    /// <list type="bullet">
    /// <item><description><c>"-"</c> prefix indicates a line removed from the original</description></item>
    /// <item><description><c>"+"</c> prefix indicates a line added in the new version</description></item>
    /// <item><description><c>" "</c> (space) prefix indicates an unchanged context line</description></item>
    /// <item><description><c>"\"</c> indicates metadata like "No newline at end of file"</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Example hunk from an Edit operation:
    /// <code>
    /// {
    ///   "oldStart": 10,
    ///   "oldLines": 3,
    ///   "newStart": 10,
    ///   "newLines": 3,
    ///   "lines": [
    ///     " // Context line before",
    ///     "-    var oldValue = 42;",
    ///     "+    var newValue = 100;",
    ///     " // Context line after"
    ///   ]
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class StructuredPatchHunk
    {
        /// <summary>
        /// Gets or sets the starting line number in the original (old) file where this hunk begins.
        /// </summary>
        /// <remarks>
        /// Line numbers are 1-based. This indicates where in the original file the changes
        /// represented by this hunk were located.
        /// </remarks>
        [JsonPropertyName("oldStart")]
        public int OldStart { get; set; }

        /// <summary>
        /// Gets or sets the number of lines from the original file affected by this hunk.
        /// </summary>
        /// <remarks>
        /// This count includes both removed lines (prefixed with <c>"-"</c>) and unchanged
        /// context lines (prefixed with <c>" "</c>) from the original file.
        /// </remarks>
        [JsonPropertyName("oldLines")]
        public int OldLines { get; set; }

        /// <summary>
        /// Gets or sets the starting line number in the new file where this hunk begins.
        /// </summary>
        /// <remarks>
        /// Line numbers are 1-based. This indicates where in the modified file the changes
        /// represented by this hunk are now located. This may differ from <see cref="OldStart"/>
        /// if previous hunks added or removed lines.
        /// </remarks>
        [JsonPropertyName("newStart")]
        public int NewStart { get; set; }

        /// <summary>
        /// Gets or sets the number of lines in the new file for this hunk.
        /// </summary>
        /// <remarks>
        /// This count includes both added lines (prefixed with <c>"+"</c>) and unchanged
        /// context lines (prefixed with <c>" "</c>) in the new file.
        /// </remarks>
        [JsonPropertyName("newLines")]
        public int NewLines { get; set; }

        /// <summary>
        /// Gets or sets the collection of diff lines representing the changes in this hunk.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Each line is prefixed with a character indicating its status:
        /// <list type="bullet">
        /// <item><description><c>"-"</c> - Line was removed from the original file</description></item>
        /// <item><description><c>"+"</c> - Line was added in the new file</description></item>
        /// <item><description><c>" "</c> - Line is unchanged (context)</description></item>
        /// <item><description><c>"\"</c> - Metadata (e.g., "\ No newline at end of file")</description></item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// [
        ///   "-    oldCode();",
        ///   "+    newCode();",
        ///   "\\ No newline at end of file"
        /// ]
        /// </code>
        /// </example>
        [JsonPropertyName("lines")]
        public List<string> Lines { get; set; } = [];
    }
}
