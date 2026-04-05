using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

    /// <summary>
    /// Represents a prompt-based permission needed to implement a plan.
    /// These describe categories of actions rather than specific commands.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Allowed prompts are used in the ExitPlanMode tool to specify which categories of
    /// tool use should be permitted when executing the plan.
    /// </para>
    /// <para>
    /// Example JSON:
    /// <code>
    /// {
    ///   "tool": "Bash",
    ///   "prompt": "run tests"
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class ExitPlanModeAllowedPrompt
    {

        /// <summary>
        /// Gets or sets the tool this prompt applies to.
        /// </summary>
        [JsonPropertyName("tool")]
        public string Tool { get; set; } = "Bash";

        /// <summary>
        /// Gets or sets the semantic description of the action, e.g. "run tests", "install dependencies".
        /// </summary>
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; } = string.Empty;

    }

}
