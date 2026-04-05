using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents git diff information for a file change, including additions, deletions,
    /// and the unified diff patch.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This type is included in tool responses for the Edit and Write tools when git tracking
    /// is available for the modified file. It provides a summary of the changes as they would
    /// appear in a <c>git diff</c> output.
    /// </para>
    /// <para>
    /// Example JSON:
    /// <code>
    /// {
    ///   "filename": "src/Program.cs",
    ///   "status": "modified",
    ///   "additions": 5,
    ///   "deletions": 2,
    ///   "changes": 7,
    ///   "patch": "@@ -10,7 +10,10 @@ ...",
    ///   "repository": "CloudNimble/ClaudeEssentials"
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class GitDiffInfo
    {

        /// <summary>
        /// Gets or sets the filename of the changed file.
        /// </summary>
        [JsonPropertyName("filename")]
        public string Filename { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the git status of the file change.
        /// </summary>
        /// <remarks>
        /// See <see cref="GitDiffStatus"/> for possible values.
        /// </remarks>
        [JsonPropertyName("status")]
        public GitDiffStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the number of lines added in this change.
        /// </summary>
        [JsonPropertyName("additions")]
        public int Additions { get; set; }

        /// <summary>
        /// Gets or sets the number of lines deleted in this change.
        /// </summary>
        [JsonPropertyName("deletions")]
        public int Deletions { get; set; }

        /// <summary>
        /// Gets or sets the total number of lines changed (additions + deletions).
        /// </summary>
        [JsonPropertyName("changes")]
        public int Changes { get; set; }

        /// <summary>
        /// Gets or sets the unified diff patch string showing the actual changes.
        /// </summary>
        [JsonPropertyName("patch")]
        public string Patch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the GitHub owner/repo identifier when available.
        /// </summary>
        /// <remarks>
        /// This is populated when the repository is associated with a GitHub remote.
        /// Format: <c>"owner/repo"</c>, for example <c>"CloudNimble/ClaudeEssentials"</c>.
        /// </remarks>
        [JsonPropertyName("repository")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Repository { get; set; }

    }

}
