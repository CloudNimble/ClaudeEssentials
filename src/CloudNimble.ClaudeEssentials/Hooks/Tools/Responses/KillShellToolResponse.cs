using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{
    /// <summary>
    /// Represents the response payload returned by the Claude Code KillShell tool after
    /// attempting to terminate a background shell process.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The KillShell tool terminates running background bash shells by their ID. Background
    /// shells are created when using the <see cref="BashToolInput.RunInBackground"/> parameter.
    /// This response is received in the <see cref="KillShellPostToolUsePayload"/>
    /// when the <c>tool_name</c> is "KillShell".
    /// </para>
    /// <para>
    /// Background shells are useful for long-running processes like development servers,
    /// file watchers, or build processes that need to run while other work continues.
    /// The KillShell tool provides a way to gracefully terminate these processes when
    /// they are no longer needed.
    /// </para>
    /// <para>
    /// Shell IDs can be found using the <c>/tasks</c> command in Claude Code.
    /// </para>
    /// <para>
    /// Example JSON payload (successful):
    /// <code>
    /// {
    ///   "shellId": "shell_abc123",
    ///   "success": true
    /// }
    /// </code>
    /// </para>
    /// <para>
    /// Example JSON payload (failed):
    /// <code>
    /// {
    ///   "shellId": "shell_invalid",
    ///   "success": false,
    ///   "error": "Shell not found or already terminated"
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class KillShellToolResponse
    {
        /// <summary>
        /// Gets or sets the identifier of the shell that was targeted for termination.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This is the shell ID that was provided in <see cref="KillShellToolInput.ShellId"/>.
        /// Shell IDs are assigned when background shells are created and follow the format
        /// <c>"shell_"</c> followed by a unique identifier.
        /// </para>
        /// <para>
        /// Available shell IDs can be discovered using the <c>/tasks</c> command in Claude Code.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// "shell_abc123xyz"
        /// </code>
        /// </example>
        [JsonPropertyName("shellId")]
        public string ShellId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the shell was successfully terminated.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When <c>true</c>, the shell process was found and terminated successfully.
        /// The shell is no longer running and its resources have been released.
        /// </para>
        /// <para>
        /// When <c>false</c>, the termination failed. Check the <see cref="Error"/> property
        /// for details about why the operation failed.
        /// </para>
        /// </remarks>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the error message if the kill operation failed.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property is populated when <see cref="Success"/> is <c>false</c>.
        /// Common error scenarios include:
        /// <list type="bullet">
        /// <item><description>The shell ID does not exist</description></item>
        /// <item><description>The shell has already been terminated</description></item>
        /// <item><description>The shell process could not be killed due to system restrictions</description></item>
        /// <item><description>The shell ID format is invalid</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// When <see cref="Success"/> is <c>true</c>, this property will be <c>null</c>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// "Shell not found or already terminated"
        /// </code>
        /// </example>
        [JsonPropertyName("error")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; }
    }
}
