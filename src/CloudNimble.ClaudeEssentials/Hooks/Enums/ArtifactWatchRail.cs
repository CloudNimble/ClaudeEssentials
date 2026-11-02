using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents a non-live delivery state of an artifact watch.
    /// </summary>
    public enum ArtifactWatchRail
    {

        /// <summary>
        /// The watch is delivered through a durable wake trigger.
        /// </summary>
        [JsonStringEnumMemberName("durable_wake")]
        DurableWake,

        /// <summary>
        /// The live watch has stopped.
        /// </summary>
        [JsonStringEnumMemberName("live_stopped")]
        LiveStopped

    }

}
