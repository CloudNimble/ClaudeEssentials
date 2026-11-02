using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the EnterWorktree tool.
    /// The EnterWorktree tool creates a temporary git worktree for isolated work.
    /// </summary>
    public class EnterWorktreeToolInput
    {

        /// <summary>
        /// Gets or sets an optional name for a new worktree. Each "/"-separated segment may contain
        /// only letters, digits, dots, underscores, and dashes; max 64 chars total.
        /// A random name is generated if not provided.
        /// </summary>
        /// <remarks>
        /// Mutually exclusive with <see cref="Path"/>.
        /// </remarks>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the path to an existing worktree of the current repository to switch into
        /// instead of creating a new one.
        /// </summary>
        /// <remarks>
        /// Must appear in <c>git worktree list</c> for the current repo. Mutually exclusive with
        /// <see cref="Name"/>.
        /// </remarks>
        [JsonPropertyName("path")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Path { get; set; }

    }

}
