using System.Text.Json.Serialization;
namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a WorktreeRemove hook.
    /// This hook runs when a git worktree is removed.
    /// </summary>
    public class WorktreeRemoveHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the path of the worktree.
        /// </summary>
        [JsonPropertyName("worktree_path")]
        public string WorktreePath { get; set; } = string.Empty;

    }

}
