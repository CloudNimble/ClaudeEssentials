using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Permissions
{

    /// <summary>
    /// Represents a permission update that removes rules.
    /// </summary>
    public class RemoveRulesPermissionUpdate : PermissionUpdate
    {

        #region Properties

        /// <summary>
        /// Gets or sets the behavior of the rules.
        /// </summary>
        [JsonPropertyName("behavior")]
        public PermissionBehavior Behavior { get; set; }

        /// <summary>
        /// Gets or sets the rules to remove.
        /// </summary>
        [JsonPropertyName("rules")]
        public List<PermissionRuleValue> Rules { get; set; } = [];

        #endregion

    }

}
