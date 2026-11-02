using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the Agent tool.
    /// The Agent tool launches specialized agents (subprocesses) that autonomously handle complex tasks.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each agent type has specific capabilities and tools available to it.
    /// Available agent types include: general-purpose, Explore, Plan, claude-code-guide, and others.
    /// </para>
    /// <para>
    /// This type reflects the latest Agent tool schema including
    /// team collaboration fields such as <see cref="Name"/>, <see cref="TeamName"/>, and <see cref="Mode"/>.
    /// </para>
    /// </remarks>
    public class AgentToolInput
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
        [JsonPropertyName("subagent_type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? SubagentType { get; set; }

        /// <summary>
        /// Gets or sets the optional model override for this agent. Takes precedence over the agent definition's
        /// model frontmatter. If omitted, uses the agent definition's model, or inherits from the parent.
        /// </summary>
        /// <remarks>
        /// See <see cref="ClaudeModel"/> for possible values.
        /// </remarks>
        [JsonPropertyName("model")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ClaudeModel? Model { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to run this agent in the background. Agents run in the background by default.
        /// </summary>
        [JsonPropertyName("run_in_background")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? RunInBackground { get; set; }

        /// <summary>
        /// Gets or sets the name for the spawned agent. Makes it addressable via SendMessage({to: name}) while running.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the team name. This value is deprecated and ignored: the session has a single implicit team.
        /// </summary>
        [JsonPropertyName("team_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TeamName { get; set; }

        /// <summary>
        /// Gets or sets the permission mode. This value is deprecated and ignored: subagents inherit the parent session's permission mode.
        /// </summary>
        [JsonPropertyName("mode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PermissionMode? Mode { get; set; }

        /// <summary>
        /// Gets or sets the isolation mode for the spawned agent.
        /// </summary>
        [JsonPropertyName("isolation")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AgentIsolationMode? Isolation { get; set; }

        /// <summary>
        /// Gets or sets the reasoning effort for this agent. Ignored when <c>subagent_type</c> is <c>fork</c>.
        /// </summary>
        [JsonPropertyName("effort")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ReasoningEffort? Effort { get; set; }

    }

}
