using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents the kind of prompt expansion.
    /// </summary>
    public enum PromptExpansionType
    {

        /// <summary>
        /// A slash command.
        /// </summary>
        [JsonStringEnumMemberName("slash_command")]
        SlashCommand,

        /// <summary>
        /// An MCP prompt.
        /// </summary>
        [JsonStringEnumMemberName("mcp_prompt")]
        McpPrompt

    }

}
