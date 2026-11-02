using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the role the user holds on a shared artifact.
    /// </summary>
    public enum ArtifactRole
    {

        /// <summary>
        /// The user can edit the artifact.
        /// </summary>
        [JsonStringEnumMemberName("editor")]
        Editor,

        /// <summary>
        /// The user can comment on the artifact.
        /// </summary>
        [JsonStringEnumMemberName("commenter")]
        Commenter,

        /// <summary>
        /// The user can read the artifact.
        /// </summary>
        [JsonStringEnumMemberName("reader")]
        Reader,

        /// <summary>
        /// The user can view the artifact.
        /// </summary>
        [JsonStringEnumMemberName("viewer")]
        Viewer

    }

}
