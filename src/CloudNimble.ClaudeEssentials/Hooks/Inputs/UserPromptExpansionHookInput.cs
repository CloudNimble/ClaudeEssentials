using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Inputs
{

    /// <summary>
    /// Represents the input received by a UserPromptExpansion hook, which runs when a slash command or MCP prompt expands into a prompt.
    /// </summary>
    public class UserPromptExpansionHookInput : HookInputBase
    {

        #region Properties

        /// <summary>
        /// Gets or sets the arguments passed to the command.
        /// </summary>
        [JsonPropertyName("command_args")]
        public string CommandArgs { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the command.
        /// </summary>
        [JsonPropertyName("command_name")]
        public string CommandName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets where the command came from.
        /// </summary>
        [JsonPropertyName("command_source")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CommandSource { get; set; }

        /// <summary>
        /// Gets or sets the kind of expansion.
        /// </summary>
        [JsonPropertyName("expansion_type")]
        public PromptExpansionType ExpansionType { get; set; }

        /// <summary>
        /// Gets or sets the expanded prompt.
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

        #endregion

    }

}
