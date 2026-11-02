using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Agent tool response when the agent was launched in a remote cloud environment.
    /// </summary>
    public class RemoteLaunchedAgentToolResponse : AgentToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the description of the task.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the path to the output file for checking agent progress.
        /// </summary>
        [JsonPropertyName("outputFile")]
        public string OutputFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the prompt for the agent.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL of the cloud session.
        /// </summary>
        [JsonPropertyName("sessionUrl")]
        public string SessionUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the ID of the remote agent task.
        /// </summary>
        [JsonPropertyName("taskId")]
        public string TaskId { get; set; } = string.Empty;

        #endregion

    }

}
