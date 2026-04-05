using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the complete output for a WorktreeCreate hook.
    /// Combines base output fields with WorktreeCreate-specific output.
    /// </summary>
    public class WorktreeCreateHookOutput : HookOutputBase
    {

        /// <summary>
        /// Gets or sets the hook-specific output containing the worktree path.
        /// </summary>
        [JsonPropertyName("hookSpecificOutput")]
        public WorktreeCreateSpecificOutput? HookSpecificOutput { get; set; }

    }

}
