using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the Agent tool response when the agent completed its work synchronously
    /// and returned its results inline.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "status": "completed",
    ///   "agentId": "agent_01ABC123",
    ///   "content": [{ "type": "text", "text": "I found 5 matching files..." }],
    ///   "totalToolUseCount": 12,
    ///   "totalDurationMs": 8500,
    ///   "totalTokens": 15000,
    ///   "usage": { "input_tokens": 10000, "output_tokens": 5000 },
    ///   "prompt": "Find all TypeScript files that import React"
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class CompletedAgentToolResponse : AgentToolResponse
    {

        /// <summary>
        /// Gets or sets the unique identifier for this agent instance.
        /// </summary>
        [JsonPropertyName("agentId")]
        public string AgentId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of agent that was used.
        /// </summary>
        [JsonPropertyName("agentType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AgentType { get; set; }

        /// <summary>
        /// Gets or sets the content blocks returned by the agent.
        /// </summary>
        [JsonPropertyName("content")]
        public List<AgentContentBlock> Content { get; set; } = [];

        /// <summary>
        /// Gets or sets the total number of tool uses made by the agent during execution.
        /// </summary>
        [JsonPropertyName("totalToolUseCount")]
        public int TotalToolUseCount { get; set; }

        /// <summary>
        /// Gets or sets the total duration of the agent execution in milliseconds.
        /// </summary>
        [JsonPropertyName("totalDurationMs")]
        public int TotalDurationMs { get; set; }

        /// <summary>
        /// Gets or sets the total number of tokens consumed by the agent.
        /// </summary>
        [JsonPropertyName("totalTokens")]
        public int TotalTokens { get; set; }

        /// <summary>
        /// Gets or sets the detailed token usage breakdown for this agent execution.
        /// </summary>
        [JsonPropertyName("usage")]
        public AgentUsageInfo Usage { get; set; } = new();

        /// <summary>
        /// Gets or sets an aggregate breakdown of the tools used by the agent during execution.
        /// </summary>
        /// <remarks>
        /// This is optional and may be omitted when the harness does not collect per-tool statistics.
        /// </remarks>
        [JsonPropertyName("toolStats")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AgentToolStats? ToolStats { get; set; }

        /// <summary>
        /// Gets or sets the original prompt that was given to the agent.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the model that ran the agent.
        /// </summary>
        [JsonPropertyName("resolvedModel")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ResolvedModel { get; set; }

        /// <summary>
        /// Gets or sets the ordered distinct models used by the agent.
        /// </summary>
        [JsonPropertyName("modelsUsed")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? ModelsUsed { get; set; }

        /// <summary>
        /// Gets or sets the names of the tools used by the agent.
        /// </summary>
        [JsonPropertyName("toolsUsed")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? ToolsUsed { get; set; }

        /// <summary>
        /// Gets or sets the path of the worktree the agent ran in.
        /// </summary>
        [JsonPropertyName("worktreePath")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? WorktreePath { get; set; }

        /// <summary>
        /// Gets or sets the branch of the worktree the agent ran in.
        /// </summary>
        [JsonPropertyName("worktreeBranch")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? WorktreeBranch { get; set; }

    }

}
