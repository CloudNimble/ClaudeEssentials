using System.Text.Json.Serialization;
namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a TeammateIdle hook.
    /// This hook runs when a teammate agent becomes idle.
    /// </summary>
    public class TeammateIdleHookInput : HookInputBase
    {

        /// <summary>
        /// Gets or sets the name of the teammate.
        /// </summary>
        [JsonPropertyName("teammate_name")]
        public string TeammateName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the team name. This value is deprecated: sessions have a single implicit team.
        /// </summary>
        [JsonPropertyName("team_name")]
        public string TeamName { get; set; } = string.Empty;

    }

}
