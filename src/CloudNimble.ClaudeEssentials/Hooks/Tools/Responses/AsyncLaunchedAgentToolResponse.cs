using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Agent tool response when the agent was launched in the background
    /// and has not yet completed.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use the <see cref="OutputFile"/> path or the TaskOutput tool with the <see cref="AgentId"/>
    /// to check the agent's progress and retrieve results once it completes.
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "status": "async_launched",
    ///   "agentId": "agent_01ABC123",
    ///   "description": "Explore codebase structure",
    ///   "prompt": "Find all TypeScript files that import React",
    ///   "outputFile": "/tmp/agent_01ABC123.jsonl",
    ///   "canReadOutputFile": true
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class AsyncLaunchedAgentToolResponse : AgentToolResponse
    {

        /// <summary>
        /// Gets or sets the ID of the async agent.
        /// </summary>
        [JsonPropertyName("agentId")]
        public string AgentId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the task.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the prompt for the agent.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the path to the output file for checking agent progress.
        /// </summary>
        [JsonPropertyName("outputFile")]
        public string OutputFile { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether the calling agent has Read/Bash tools to check progress.
        /// </summary>
        [JsonPropertyName("canReadOutputFile")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? CanReadOutputFile { get; set; }

    }

}
