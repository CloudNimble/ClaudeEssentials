using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Outputs
{

    /// <summary>
    /// Represents the hook-specific output for a PreModelSwitch hook.
    /// </summary>
    public class PreModelSwitchSpecificOutput : HookSpecificOutputBase
    {

        #region Properties

        /// <summary>
        /// Gets the hook event name for this output type.
        /// </summary>
        [JsonPropertyName("hookEventName")]
        public override HookEventName HookEventName => HookEventName.PreModelSwitch;

        /// <summary>
        /// Gets or sets a value indicating whether the switch proceeds. Allow skips the confirmation, deny cancels the switch, and ask has the user confirm.
        /// </summary>
        [JsonPropertyName("permissionDecision")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PermissionDecision? PermissionDecision { get; set; }

        /// <summary>
        /// Gets or sets the reason for the decision.
        /// </summary>
        [JsonPropertyName("permissionDecisionReason")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PermissionDecisionReason { get; set; }

        #endregion

    }

}
