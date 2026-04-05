using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a TaskCompleted hook.
    /// This hook runs when a task completes.
    /// </summary>
    public class TaskCompletedHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the detailed description of the task.
        /// </summary>
        [JsonPropertyName("task_description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TaskDescription { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the task.
        /// </summary>
        [JsonPropertyName("task_id")]
        public string TaskId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the title of the task.
        /// </summary>
        [JsonPropertyName("task_subject")]
        public string TaskSubject { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the team associated with this task.
        /// </summary>
        [JsonPropertyName("team_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TeamName { get; set; }

        /// <summary>
        /// Gets or sets the name of the teammate completing the task.
        /// </summary>
        [JsonPropertyName("teammate_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TeammateName { get; set; }

    }

}
