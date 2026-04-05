using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a WorktreeCreate hook.
    /// Contains the path for the created worktree.
    /// </summary>
    public class WorktreeCreateSpecificOutput : HookSpecificOutputBase
    {

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.WorktreeCreate;

        /// <summary>
        /// Gets or sets the file system path where the worktree was created.
        /// Hook failure or a missing path will fail the worktree creation.
        /// </summary>
        [JsonPropertyName("worktreePath")]
        public string? WorktreePath { get; set; }

    }

}
