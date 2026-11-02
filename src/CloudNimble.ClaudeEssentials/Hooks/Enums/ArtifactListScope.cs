using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents which artifacts a list operation covers.
    /// </summary>
    public enum ArtifactListScope
    {

        /// <summary>
        /// Artifacts the user owns. This is the default.
        /// </summary>
        [JsonStringEnumMemberName("mine")]
        Mine,

        /// <summary>
        /// Artifacts other people shared with the user.
        /// </summary>
        [JsonStringEnumMemberName("shared")]
        Shared,

        /// <summary>
        /// Both owned and shared artifacts.
        /// </summary>
        [JsonStringEnumMemberName("all")]
        All

    }

}
