using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Read tool response for a standard text file.
    /// </summary>
    /// <remarks>
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
    public class TextReadToolResponse : ReadToolResponse
    {

        /// <summary>
        /// Gets or sets the file information containing the path, content, and line metadata.
        /// </summary>
        [JsonPropertyName("file")]
        public ReadToolFileInfo File { get; set; } = new();

        /// <summary>
        /// Gets or sets the artifact and version that now count as viewed, set when this Read completed a saved Artifact source file.
        /// </summary>
        [JsonPropertyName("artifactRead")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ArtifactReadInfo? ArtifactRead { get; set; }

    }

}
