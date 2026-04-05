using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the Claude Code ExitPlanMode tool after
    /// transitioning from plan mode to execution mode.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The ExitPlanMode tool is used when Claude has finished creating a plan and is ready to
    /// begin executing it. The response includes the plan text and metadata about how it was
    /// presented to the user.
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "plan": "## Implementation Plan\n\n1. Create new models...",
    ///   "isAgent": false,
    ///   "filePath": "/tmp/plan-abc123.md",
    ///   "hasTaskTool": true,
    ///   "planWasEdited": false
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class ExitPlanModeToolResponse
    {

        /// <summary>
        /// Gets or sets the plan that was presented to the user.
        /// </summary>
        [JsonPropertyName("plan")]
        public string? Plan { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is an agent context.
        /// </summary>
        [JsonPropertyName("isAgent")]
        public bool IsAgent { get; set; }

        /// <summary>
        /// Gets or sets the file path where the plan was saved.
        /// </summary>
        [JsonPropertyName("filePath")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FilePath { get; set; }

        /// <summary>
        /// Gets or sets whether the Agent tool is available in the current context.
        /// </summary>
        [JsonPropertyName("hasTaskTool")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? HasTaskTool { get; set; }

        /// <summary>
        /// Gets or sets whether the user edited the plan (CCR web UI or Ctrl+G).
        /// Determines whether the plan is echoed back in tool_result.
        /// </summary>
        [JsonPropertyName("planWasEdited")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? PlanWasEdited { get; set; }

        /// <summary>
        /// Gets or sets whether the teammate has sent a plan approval request to the team leader.
        /// </summary>
        [JsonPropertyName("awaitingLeaderApproval")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? AwaitingLeaderApproval { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the plan approval request.
        /// </summary>
        [JsonPropertyName("requestId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RequestId { get; set; }

    }

}
