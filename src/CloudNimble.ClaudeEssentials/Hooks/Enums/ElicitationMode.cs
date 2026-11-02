using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks
{

    /// <summary>
    /// Represents how an MCP elicitation collects input.
    /// </summary>
    public enum ElicitationMode
    {

        /// <summary>
        /// Collects input through a form.
        /// </summary>
        [JsonStringEnumMemberName("form")]
        Form,

        /// <summary>
        /// Collects input by sending the user to a URL.
        /// </summary>
        [JsonStringEnumMemberName("url")]
        Url

    }

}
