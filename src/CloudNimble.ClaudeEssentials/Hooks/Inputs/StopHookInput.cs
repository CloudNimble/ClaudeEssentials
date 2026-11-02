using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a Stop hook.
    /// This hook runs when Claude Code finishes responding.
    /// </summary>
    public class StopHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets a value indicating whether the stop hook is already active.
        /// True if already continuing from a previous stop hook.
        /// </summary>
        [JsonPropertyName("stop_hook_active")]
        public bool StopHookActive { get; set; }

        /// <summary>
        /// Gets or sets the text content of the last assistant message before stopping.
        /// </summary>
        [JsonPropertyName("last_assistant_message")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LastAssistantMessage { get; set; }

        /// <summary>
        /// Gets or sets the in-flight background work registered in this session.
        /// </summary>
        [JsonPropertyName("background_tasks")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<BackgroundTaskSummary>? BackgroundTasks { get; set; }

        /// <summary>
        /// Gets or sets the session-scoped cron tasks that will wake this session later.
        /// </summary>
        [JsonPropertyName("session_crons")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<SessionCronSummary>? SessionCrons { get; set; }

    }

}
