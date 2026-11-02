using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents why the Read tool reported a file as unchanged without a prior Read result.
    /// </summary>
    public enum FileUnchangedSource
    {

        /// <summary>
        /// The deduplication matched a startup-seeded entry (CLAUDE.md or nested memory) rather than a prior Read tool result.
        /// </summary>
        [JsonStringEnumMemberName("seeded")]
        Seeded

    }

}
