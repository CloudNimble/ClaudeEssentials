using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents a proposal to create or improve a skill.
    /// </summary>
    public class SkillProposal
    {

        #region Properties

        /// <summary>
        /// Gets or sets one short sentence saying when to use this skill.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the memory file paths where this procedure was observed.
        /// </summary>
        [JsonPropertyName("evidence")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Evidence { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a new skill or an improvement to an existing one.
        /// </summary>
        [JsonPropertyName("kind")]
        public SkillProposalKind Kind { get; set; }

        /// <summary>
        /// Gets or sets the kebab-case skill slug.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the complete SKILL.md exactly as it should be saved.
        /// </summary>
        [JsonPropertyName("skillMd")]
        public string SkillMd { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the existing skill to update. Required when <c>kind</c> is improvement.
        /// </summary>
        [JsonPropertyName("target")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Target { get; set; }

        #endregion

    }

}
