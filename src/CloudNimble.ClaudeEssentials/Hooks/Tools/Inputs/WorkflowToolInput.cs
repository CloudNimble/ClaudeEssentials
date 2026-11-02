using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the Workflow tool, which runs a script that orchestrates multiple subagents.
    /// </summary>
    public class WorkflowToolInput
    {

        #region Properties

        /// <summary>
        /// Gets or sets the optional input value exposed to the script as the global <c>args</c>, verbatim.
        /// </summary>
        [JsonPropertyName("args")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public JsonElement? Args { get; set; }

        /// <summary>
        /// Gets or sets the description. This value is ignored: set the description in the script's <c>meta</c> block.
        /// </summary>
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the name of a predefined workflow, which resolves to a self-contained script.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the run ID of a prior invocation in the same session to resume from.
        /// </summary>
        [JsonPropertyName("resumeFromRunId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ResumeFromRunId { get; set; }

        /// <summary>
        /// Gets or sets the self-contained workflow script.
        /// </summary>
        [JsonPropertyName("script")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Script { get; set; }

        /// <summary>
        /// Gets or sets the path to a workflow script file on disk. Takes precedence over <c>script</c> and <c>name</c>.
        /// </summary>
        [JsonPropertyName("scriptPath")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ScriptPath { get; set; }

        /// <summary>
        /// Gets or sets the title. This value is ignored: set the title in the script's <c>meta</c> block.
        /// </summary>
        [JsonPropertyName("title")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }

        #endregion

    }

}
