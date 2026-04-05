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
        /// Gets or sets an optional name for the worktree. Each "/"-separated segment may contain
        /// only letters, digits, dots, underscores, and dashes; max 64 chars total.
        /// A random name is generated if not provided.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

    }

}
