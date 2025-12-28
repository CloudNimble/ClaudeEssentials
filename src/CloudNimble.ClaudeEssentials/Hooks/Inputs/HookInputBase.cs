using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Enums;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Base class containing common fields present in all hook inputs.
    /// All hooks receive these fields via JSON through stdin.
    /// </summary>
    public abstract class HookInputBase
    {

        /// <summary>
        /// Gets or sets the unique identifier for the current Claude Code session.
        /// </summary>
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the file path to the transcript JSONL file for the current session.
        /// This file contains the full conversation history.
        /// </summary>
        [JsonPropertyName("transcript_path")]
        public string TranscriptPath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the current working directory where Claude Code is running.
        /// </summary>
        [JsonPropertyName("cwd")]
        public string CurrentWorkingDirectory { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the permission mode under which Claude Code is operating.
        /// </summary>
        [JsonPropertyName("permission_mode")]
        public PermissionMode PermissionMode { get; set; }

        /// <summary>
        /// Gets or sets the name of the hook event that triggered this input.
        /// </summary>
        [JsonPropertyName("hook_event_name")]
        public HookEventName HookEventName { get; set; }

    }

}
