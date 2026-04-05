using CloudNimble.ClaudeEssentials.Hooks;
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
    /// Unlike <see cref="TaskToolInput"/>, this type reflects the latest Agent tool schema including
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
        /// Gets or sets whether to run this agent in the background.
        /// You will be notified when it completes.
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
        /// Gets or sets the team name for spawning. Uses current team context if omitted.
        /// </summary>
        [JsonPropertyName("team_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TeamName { get; set; }

        /// <summary>
        /// Gets or sets the permission mode for the spawned teammate (e.g., "plan" to require plan approval).
        /// </summary>
        /// <remarks>
        /// See <see cref="PermissionMode"/> for possible values.
        /// </remarks>
        [JsonPropertyName("mode")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PermissionMode? Mode { get; set; }

        /// <summary>
        /// Gets or sets the isolation mode. Set to <c>"worktree"</c> to create a temporary git worktree
        /// so the agent works on an isolated copy of the repo.
        /// </summary>
        [JsonPropertyName("isolation")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Isolation { get; set; }

    }

}
