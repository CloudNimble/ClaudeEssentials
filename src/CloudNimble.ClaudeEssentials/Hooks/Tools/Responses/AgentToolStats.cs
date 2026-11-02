using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents an aggregate breakdown of the tools used by an Agent during its execution.
    /// </summary>
    /// <remarks>
    /// <para>
    /// These counts give a high-level view of the agent's workload, useful for understanding
    /// whether the agent spent its time searching, reading, editing, or running shell commands.
    /// </para>
    /// <para>
    /// The <see cref="LinesAdded"/> and <see cref="LinesRemoved"/> counts aggregate line-level
    /// changes across all Edit/Write operations performed by the agent.
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "readCount": 12,
    ///   "searchCount": 5,
    ///   "bashCount": 3,
    ///   "editFileCount": 4,
    ///   "linesAdded": 87,
    ///   "linesRemoved": 23,
    ///   "otherToolCount": 2
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class AgentToolStats
    {

        /// <summary>
        /// Gets or sets the number of file read operations performed by the agent.
        /// </summary>
        [JsonPropertyName("readCount")]
        public int ReadCount { get; set; }

        /// <summary>
        /// Gets or sets the number of search operations (Grep, Glob, etc.) performed by the agent.
        /// </summary>
        [JsonPropertyName("searchCount")]
        public int SearchCount { get; set; }

        /// <summary>
        /// Gets or sets the number of Bash shell commands executed by the agent.
        /// </summary>
        [JsonPropertyName("bashCount")]
        public int BashCount { get; set; }

        /// <summary>
        /// Gets or sets the number of file edit or write operations performed by the agent.
        /// </summary>
        [JsonPropertyName("editFileCount")]
        public int EditFileCount { get; set; }

        /// <summary>
        /// Gets or sets the total number of lines added across all edit and write operations.
        /// </summary>
        [JsonPropertyName("linesAdded")]
        public int LinesAdded { get; set; }

        /// <summary>
        /// Gets or sets the total number of lines removed across all edit and write operations.
        /// </summary>
        [JsonPropertyName("linesRemoved")]
        public int LinesRemoved { get; set; }

        /// <summary>
        /// Gets or sets the number of other tool invocations not covered by the specific counters.
        /// </summary>
        [JsonPropertyName("otherToolCount")]
        public int OtherToolCount { get; set; }

        /// <summary>
        /// Gets or sets the number of frames captured by the agent.
        /// </summary>
        [JsonPropertyName("frameCount")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? FrameCount { get; set; }

    }

}
