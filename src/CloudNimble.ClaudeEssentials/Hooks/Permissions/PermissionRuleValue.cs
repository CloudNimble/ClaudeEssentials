using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Permissions
{

    /// <summary>
    /// Represents a permission rule.
    /// </summary>
    public class PermissionRuleValue
    {

        #region Properties

        /// <summary>
        /// Gets or sets the rule's content, which narrows it to specific uses of the tool.
        /// </summary>
        [JsonPropertyName("ruleContent")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RuleContent { get; set; }

        /// <summary>
        /// Gets or sets the name of the tool the rule applies to.
        /// </summary>
        [JsonPropertyName("toolName")]
        public string ToolName { get; set; } = string.Empty;

        #endregion

    }

}
