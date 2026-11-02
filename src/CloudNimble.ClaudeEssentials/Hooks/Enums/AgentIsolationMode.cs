using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the isolation mode used when spawning an agent.
    /// </summary>
    public enum AgentIsolationMode
    {

        /// <summary>
        /// Creates a temporary git worktree so the agent works on an isolated copy of the repo.
        /// </summary>
        [JsonStringEnumMemberName("worktree")]
        Worktree,

        /// <summary>
        /// Launches the agent in a remote cloud environment. The agent always runs in the background.
        /// </summary>
        [JsonStringEnumMemberName("remote")]
        Remote

    }

}
