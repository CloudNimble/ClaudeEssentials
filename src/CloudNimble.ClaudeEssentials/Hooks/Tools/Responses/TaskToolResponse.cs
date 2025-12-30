using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{
    /// <summary>
    /// Represents the response payload returned by the Claude Code Task tool after executing
    /// a subagent to handle a complex task.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Task tool launches specialized agents (subprocesses) that autonomously handle
    /// complex, multi-step tasks. Each agent type has specific capabilities and tools available
    /// to it. This response is received in the <see cref="TaskPostToolUsePayload"/>
    /// when the <c>tool_name</c> is "Task".
    /// </para>
    /// <para>
    /// Available agent types include:
    /// <list type="bullet">
    /// <item><description><c>general-purpose</c> - For research, code search, and multi-step tasks</description></item>
    /// <item><description><c>Explore</c> - Fast codebase exploration and pattern searches</description></item>
    /// <item><description><c>Plan</c> - Software architecture and implementation planning</description></item>
    /// <item><description><c>claude-code-guide</c> - Documentation and guidance queries</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Agents can be resumed using the <see cref="AgentId"/> returned in the response,
    /// allowing continuation of previous work with full context preserved.
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "result": "I found 5 files that match your search criteria...",
    ///   "agentId": "agent_01ABC123XYZ",
    ///   "status": "completed"
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class TaskToolResponse
    {
        /// <summary>
        /// Gets or sets the result message returned by the agent upon completion.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This contains the agent's response after completing (or attempting to complete)
        /// the assigned task. The content varies based on what was requested and what
        /// the agent discovered or accomplished.
        /// </para>
        /// <para>
        /// Note that agent results are not automatically visible to the user. Claude must
        /// summarize or relay the information in its response to share it with the user.
        /// </para>
        /// </remarks>
        [JsonPropertyName("result")]
        public string Result { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier for this agent instance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This ID can be used to resume the agent later via the <see cref="TaskToolInput.Resume"/>
        /// parameter. When resumed, the agent continues with its full previous context preserved,
        /// allowing for follow-up work or continuation of interrupted tasks.
        /// </para>
        /// <para>
        /// Agent IDs are unique within a session and follow the format <c>"agent_"</c> followed
        /// by an identifier string.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// "agent_01ABC123XYZ789"
        /// </code>
        /// </example>
        [JsonPropertyName("agentId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AgentId { get; set; }

        /// <summary>
        /// Gets or sets the completion status of the task execution.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Indicates the final state of the agent's work. Common values include:
        /// <list type="bullet">
        /// <item><description><c>"completed"</c> - The agent finished its task successfully</description></item>
        /// <item><description><c>"running"</c> - The agent is still working (for background tasks)</description></item>
        /// <item><description><c>"error"</c> - The agent encountered an error</description></item>
        /// <item><description><c>"interrupted"</c> - The agent was stopped before completion</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// For agents run in the background (using <see cref="TaskToolInput.RunInBackground"/>),
        /// use the TaskOutput tool to check the status and retrieve results once the agent completes.
        /// </para>
        /// </remarks>
        [JsonPropertyName("status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Status { get; set; }
    }
}
