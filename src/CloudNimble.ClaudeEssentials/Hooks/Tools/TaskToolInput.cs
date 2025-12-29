using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the input parameters for the Task tool.
    /// The Task tool launches specialized agents (subprocesses) that autonomously handle complex tasks.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each agent type has specific capabilities and tools available to it.
    /// Available agent types include: general-purpose, Explore, Plan, and others.
    /// </para>
    /// </remarks>
    public class TaskToolInput
    {

        /// <summary>
        /// Gets or sets a short (3-5 word) description of the task.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the task for the agent to perform.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of specialized agent to use for this task.
        /// </summary>
        /// <remarks>
        /// Examples: "general-purpose", "Explore", "Plan", "statusline-setup"
        /// </remarks>
        [JsonPropertyName("subagent_type")]
        public string SubagentType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the optional model to use for this agent.
        /// </summary>
        /// <remarks>
        /// If not specified, inherits from parent. Options: "sonnet", "opus", "haiku".
        /// Prefer haiku for quick, straightforward tasks to minimize cost and latency.
        /// </remarks>
        [JsonPropertyName("model")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Model { get; set; }

        /// <summary>
        /// Gets or sets an optional agent ID to resume from.
        /// </summary>
        /// <remarks>
        /// If provided, the agent will continue from the previous execution transcript.
        /// </remarks>
        [JsonPropertyName("resume")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Resume { get; set; }

        /// <summary>
        /// Gets or sets whether to run this agent in the background.
        /// </summary>
        /// <remarks>
        /// When <c>true</c>, use TaskOutput to read the output later.
        /// </remarks>
        [JsonPropertyName("run_in_background")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? RunInBackground { get; set; }

    }

}
