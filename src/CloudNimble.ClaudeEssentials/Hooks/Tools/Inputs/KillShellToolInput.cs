using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents the input parameters for the KillShell tool.
    /// The KillShell tool terminates a running background bash shell by its ID.
    /// </summary>
    public class KillShellToolInput
    {

        /// <summary>
        /// Gets or sets the ID of the background shell to kill.
        /// </summary>
        /// <remarks>
        /// Shell IDs can be found using the /tasks command.
        /// </remarks>
        [JsonPropertyName("shell_id")]
        public string ShellId { get; set; } = string.Empty;

    }

}
