using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{
    /// <summary>
    /// Represents the response payload returned by the Claude Code Bash tool after executing
    /// a shell command.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Bash tool executes shell commands in a persistent shell session. This response is
    /// received in the <see cref="BashPostToolUsePayload"/>
    /// when the <c>tool_name</c> is "Bash".
    /// </para>
    /// <para>
    /// Commands have a default timeout of 120 seconds (2 minutes), which can be extended
    /// up to 600 seconds (10 minutes) using the <see cref="BashToolInput.Timeout"/> parameter.
    /// If a command exceeds its timeout, it will be interrupted and <see cref="Interrupted"/>
    /// will be <c>true</c>.
    /// </para>
    /// <para>
    /// Output exceeding 30,000 characters is automatically truncated by Claude Code.
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "stdout": "Hello, World!\r\n",
    ///   "stderr": "",
    ///   "interrupted": false,
    ///   "isImage": false
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class BashToolResponse
    {
        /// <summary>
        /// Gets or sets the standard output (stdout) from the executed command.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This contains all text written to stdout by the command. On Windows, line endings
        /// will typically be CRLF (<c>\r\n</c>), while on Unix-like systems they will be LF (<c>\n</c>).
        /// </para>
        /// <para>
        /// If the output exceeds 30,000 characters, it will be truncated by Claude Code.
        /// </para>
        /// <para>
        /// For commands that produce binary output (like image generation), this may be empty
        /// and <see cref="IsImage"/> will be <c>true</c>.
        /// </para>
        /// </remarks>
        [JsonPropertyName("stdout")]
        public string Stdout { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the standard error (stderr) output from the executed command.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This contains all text written to stderr by the command. Many commands write
        /// warnings, progress information, or diagnostic messages to stderr even when
        /// successful.
        /// </para>
        /// <para>
        /// An empty <see cref="Stderr"/> does not necessarily indicate success, and a
        /// non-empty <see cref="Stderr"/> does not necessarily indicate failure. Check
        /// the actual content and context to determine the command's success.
        /// </para>
        /// </remarks>
        [JsonPropertyName("stderr")]
        public string Stderr { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the command was interrupted before completion.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When <c>true</c>, the command did not complete normally. This typically occurs when:
        /// <list type="bullet">
        /// <item><description>The command exceeded its timeout (default 120 seconds, max 600 seconds)</description></item>
        /// <item><description>The user manually cancelled the operation</description></item>
        /// <item><description>The command was killed by the system</description></item>
        /// </list>
        /// </para>
        /// <para>
        /// When a command is interrupted, <see cref="Stdout"/> and <see cref="Stderr"/> will
        /// contain whatever output was captured before the interruption occurred.
        /// </para>
        /// </remarks>
        [JsonPropertyName("interrupted")]
        public bool Interrupted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the output contains image data.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When <c>true</c>, the command produced image output that Claude can interpret
        /// visually. This is used for commands that generate images or graphical output.
        /// </para>
        /// <para>
        /// Claude is a multimodal LLM and can process image data directly. When this is
        /// <c>true</c>, the <see cref="Stdout"/> may contain base64-encoded image data
        /// or be empty while the image is presented to Claude separately.
        /// </para>
        /// </remarks>
        [JsonPropertyName("isImage")]
        public bool IsImage { get; set; }
    }
}
