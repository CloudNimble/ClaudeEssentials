using System.Text.Json.Serialization;
using CloudNimble.ClaudeEssentials.Hooks.Enums;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a SessionStart hook.
    /// This hook runs when Claude Code starts a new session or resumes one.
    /// </summary>
    public class SessionStartHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the source that triggered the session start.
        /// </summary>
        [JsonPropertyName("source")]
        public SessionStartSource Source { get; set; }

        /// <summary>
        /// Gets or sets the file path where environment variables can be persisted.
        /// This file can be written to during SessionStart to set environment variables
        /// that will be available throughout the session.
        /// </summary>
        [JsonPropertyName("env_file")]
        public string? EnvironmentFilePath { get; set; }

    }

}
