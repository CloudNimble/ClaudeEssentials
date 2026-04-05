using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the ListMcpResources tool.
    /// The ListMcpResources tool lists available MCP (Model Context Protocol) resources.
    /// </summary>
    public class ListMcpResourcesToolInput
    {

        /// <summary>
        /// Gets or sets an optional server name to filter resources by.
        /// </summary>
        [JsonPropertyName("server")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Server { get; set; }

    }

}
