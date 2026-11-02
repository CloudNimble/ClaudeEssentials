using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the Workflow tool.
    /// </summary>
    public class WorkflowToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the error message, set if the syntax check failed.
        /// </summary>
        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; }

        /// <summary>
        /// Gets or sets the local workflow run identifier to pass as <c>resumeFromRunId</c>.
        /// </summary>
        [JsonPropertyName("runId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RunId { get; set; }

        /// <summary>
        /// Gets or sets the path to the persisted workflow script for this invocation.
        /// </summary>
        [JsonPropertyName("scriptPath")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ScriptPath { get; set; }

        /// <summary>
        /// Gets or sets the cloud session URL when the status is remote_launched.
        /// </summary>
        [JsonPropertyName("sessionUrl")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SessionUrl { get; set; }

        /// <summary>
        /// Gets or sets how the workflow was launched.
        /// </summary>
        [JsonPropertyName("status")]
        public WorkflowLaunchStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the launch summary.
        /// </summary>
        [JsonPropertyName("summary")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Summary { get; set; }

        /// <summary>
        /// Gets or sets the ID of the background task.
        /// </summary>
        [JsonPropertyName("taskId")]
        public string TaskId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of the registered background task.
        /// </summary>
        [JsonPropertyName("taskType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public WorkflowTaskType? TaskType { get; set; }

        /// <summary>
        /// Gets or sets the directory where subagent transcripts are written during execution.
        /// </summary>
        [JsonPropertyName("transcriptDir")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TranscriptDir { get; set; }

        /// <summary>
        /// Gets or sets a non-blocking heads-up, such as local git state diverging from the pushed branch.
        /// </summary>
        [JsonPropertyName("warning")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Warning { get; set; }

        /// <summary>
        /// Gets or sets the <c>meta.name</c> value from the workflow script.
        /// </summary>
        [JsonPropertyName("workflowName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? WorkflowName { get; set; }

        #endregion

    }

}
