using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the pin operation that was performed on an artifact.
    /// </summary>
    public enum ArtifactPinAction
    {

        /// <summary>
        /// The artifact was pinned.
        /// </summary>
        [JsonStringEnumMemberName("pin")]
        Pin,

        /// <summary>
        /// The artifact was unpinned.
        /// </summary>
        [JsonStringEnumMemberName("unpin")]
        Unpin

    }

}
