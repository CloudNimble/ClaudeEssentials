using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents whether a proposed skill is new or improves an existing one.
    /// </summary>
    public enum SkillProposalKind
    {

        /// <summary>
        /// A new skill.
        /// </summary>
        [JsonStringEnumMemberName("new")]
        New,

        /// <summary>
        /// An improvement to an existing skill.
        /// </summary>
        [JsonStringEnumMemberName("improvement")]
        Improvement

    }

}
