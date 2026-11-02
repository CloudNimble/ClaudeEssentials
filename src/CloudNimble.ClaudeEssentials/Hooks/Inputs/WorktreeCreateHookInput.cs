using System.Text.Json.Serialization;
namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a WorktreeCreate hook.
    /// This hook runs when a git worktree is created.
    /// </summary>
    public class WorktreeCreateHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the name of the worktree.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

    }

}
