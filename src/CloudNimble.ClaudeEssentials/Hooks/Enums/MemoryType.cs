using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the type of instruction memory file loaded in Claude Code.
    /// </summary>
    public enum MemoryType
    {

        /// <summary>
        /// A user-level instruction file (e.g., ~/.claude/CLAUDE.md).
        /// </summary>
        [JsonStringEnumMemberName("User")]
        User,

        /// <summary>
        /// A project-level instruction file checked into the repository.
        /// </summary>
        [JsonStringEnumMemberName("Project")]
        Project,

        /// <summary>
        /// A local instruction file that is not checked into version control.
        /// </summary>
        [JsonStringEnumMemberName("Local")]
        Local,

        /// <summary>
        /// A managed instruction file maintained by a plugin or external system.
        /// </summary>
        [JsonStringEnumMemberName("Managed")]
        Managed

    }

}
