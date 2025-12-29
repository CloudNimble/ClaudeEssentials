using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the input parameters for the Bash tool.
    /// The Bash tool executes shell commands in a persistent bash session.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This tool is for terminal operations like git, npm, docker, etc.
    /// </para>
    /// <para>
    /// If the output exceeds 30000 characters, it will be truncated.
    /// </para>
    /// </remarks>
    public class BashToolInput
    {

        /// <summary>
        /// Gets or sets the command to execute.
        /// </summary>
        [JsonPropertyName("command")]
        public string Command { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a clear, concise description of what this command does in 5-10 words.
        /// </summary>
        /// <remarks>
        /// Should be in active voice. For example: "List files in current directory" or "Install package dependencies".
        /// </remarks>
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the optional timeout in milliseconds.
        /// </summary>
        /// <remarks>
        /// Maximum is 600000 (10 minutes). Default is 120000 (2 minutes).
        /// </remarks>
        [JsonPropertyName("timeout")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Timeout { get; set; }

        /// <summary>
        /// Gets or sets whether to run this command in the background.
        /// </summary>
        /// <remarks>
        /// When <c>true</c>, you can monitor the output using subsequent Bash tool calls.
        /// You do not need to use '&amp;' at the end of the command when using this parameter.
        /// </remarks>
        [JsonPropertyName("run_in_background")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? RunInBackground { get; set; }

        /// <summary>
        /// Gets or sets whether to dangerously override sandbox mode and run commands without sandboxing.
        /// </summary>
        [JsonPropertyName("dangerouslyDisableSandbox")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? DangerouslyDisableSandbox { get; set; }

    }

}
