using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents where a permission update is saved.
    /// </summary>
    public enum PermissionUpdateDestination
    {

        /// <summary>
        /// The user settings file.
        /// </summary>
        [JsonStringEnumMemberName("userSettings")]
        UserSettings,

        /// <summary>
        /// The project settings file.
        /// </summary>
        [JsonStringEnumMemberName("projectSettings")]
        ProjectSettings,

        /// <summary>
        /// The local project settings file.
        /// </summary>
        [JsonStringEnumMemberName("localSettings")]
        LocalSettings,

        /// <summary>
        /// The current session only.
        /// </summary>
        [JsonStringEnumMemberName("session")]
        Session,

        /// <summary>
        /// The command-line arguments.
        /// </summary>
        [JsonStringEnumMemberName("cliArg")]
        CliArg

    }

}
