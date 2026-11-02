using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents how a directory was added to a session.
    /// </summary>
    public enum DirectoryAddedSource
    {

        /// <summary>
        /// The directory was added with the <c>/add-dir</c> command.
        /// </summary>
        [JsonStringEnumMemberName("slash_command")]
        SlashCommand,

        /// <summary>
        /// The directory was added through the SDK control request.
        /// </summary>
        [JsonStringEnumMemberName("register_repo_root")]
        RegisterRepoRoot

    }

}
