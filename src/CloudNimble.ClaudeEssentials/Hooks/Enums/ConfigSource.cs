using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the source of a Claude Code configuration change.
    /// </summary>
    public enum ConfigSource
    {

        /// <summary>
        /// User-level settings that apply globally.
        /// </summary>
        [JsonStringEnumMemberName("user_settings")]
        UserSettings,

        /// <summary>
        /// Project-level settings checked into the repository.
        /// </summary>
        [JsonStringEnumMemberName("project_settings")]
        ProjectSettings,

        /// <summary>
        /// Local settings that are not checked into version control.
        /// </summary>
        [JsonStringEnumMemberName("local_settings")]
        LocalSettings,

        /// <summary>
        /// Organization policy settings that override other settings.
        /// </summary>
        [JsonStringEnumMemberName("policy_settings")]
        PolicySettings,

        /// <summary>
        /// Settings provided by skills or plugins.
        /// </summary>
        [JsonStringEnumMemberName("skills")]
        Skills

    }

}
