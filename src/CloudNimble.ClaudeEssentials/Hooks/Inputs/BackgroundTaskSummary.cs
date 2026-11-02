using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents in-flight background work registered in a session.
    /// </summary>
    public class BackgroundTaskSummary
    {

        #region Properties

        /// <summary>
        /// Gets or sets the subagent type name. Present only for subagent tasks.
        /// </summary>
        [JsonPropertyName("agent_type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AgentType { get; set; }

        /// <summary>
        /// Gets or sets the shell command line. Present only for shell tasks.
        /// </summary>
        [JsonPropertyName("command")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Command { get; set; }

        /// <summary>
        /// Gets or sets the free-text description, capped at 1000 characters.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the task ID.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the workflow name. Present only for workflow tasks.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the MCP server name. Present only for monitor and MCP tasks.
        /// </summary>
        [JsonPropertyName("server")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Server { get; set; }

        /// <summary>
        /// Gets or sets the task status.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the MCP tool name. Present only for monitor and MCP tasks.
        /// </summary>
        [JsonPropertyName("tool")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Tool { get; set; }

        /// <summary>
        /// Gets or sets the friendly task-type label, such as <c>shell</c> or <c>subagent</c>.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        #endregion

    }

}
