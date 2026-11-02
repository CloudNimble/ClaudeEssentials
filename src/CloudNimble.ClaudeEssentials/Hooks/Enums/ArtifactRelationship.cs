using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents how a listed artifact relates to the user.
    /// </summary>
    public enum ArtifactRelationship
    {

        /// <summary>
        /// The user owns the artifact.
        /// </summary>
        [JsonStringEnumMemberName("mine")]
        Mine,

        /// <summary>
        /// The artifact was shared with the user.
        /// </summary>
        [JsonStringEnumMemberName("shared")]
        Shared

    }

}
