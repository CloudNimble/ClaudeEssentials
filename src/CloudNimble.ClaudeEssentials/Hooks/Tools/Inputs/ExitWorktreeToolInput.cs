using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the ExitWorktree tool.
    /// The ExitWorktree tool exits and optionally removes a git worktree.
    /// </summary>
    public class ExitWorktreeToolInput
    {

        /// <summary>
        /// Gets or sets the action to take. <see cref="WorktreeAction.Keep"/> leaves the worktree
        /// and branch on disk; <see cref="WorktreeAction.Remove"/> deletes both.
        /// </summary>
        [JsonPropertyName("action")]
        public WorktreeAction Action { get; set; }

        /// <summary>
        /// Gets or sets whether to force removal when the worktree has uncommitted files or unmerged commits.
        /// Required to be <c>true</c> when <see cref="Action"/> is <c>"remove"</c> and there are uncommitted
        /// files or unmerged commits. The tool will refuse and list them otherwise.
        /// </summary>
        [JsonPropertyName("discard_changes")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? DiscardChanges { get; set; }

    }

}
