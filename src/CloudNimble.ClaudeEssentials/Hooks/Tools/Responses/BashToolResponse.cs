using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;
using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;

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
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsImage { get; set; }

        /// <summary>
        /// Gets or sets a model-facing note listing <c>readFileState</c> entries whose modification
        /// time changed during the execution of this command.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Claude Code tracks the modification time of files it has previously read. When a Bash
        /// command matches the harness's <c>WRITE_COMMAND_MARKERS</c> heuristic (for example,
        /// commands that write, move, or generate files), the harness checks whether any tracked
        /// files had their mtime bumped while the command was running and surfaces that information
        /// here.
        /// </para>
        /// <para>
        /// This hint exists so the model can re-read stale files before acting on cached content;
        /// it is <c>null</c> when the heuristic did not fire or no tracked files were touched.
        /// </para>
        /// </remarks>
        [JsonPropertyName("staleReadFileStateHint")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StaleReadFileStateHint { get; set; }

        /// <summary>
        /// Gets or sets the path to the raw output file for large MCP tool outputs.
        /// </summary>
        [JsonPropertyName("rawOutputPath")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RawOutputPath { get; set; }

        /// <summary>
        /// Gets or sets the ID of the background task if the command is running in the background.
        /// </summary>
        [JsonPropertyName("backgroundTaskId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BackgroundTaskId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user manually backgrounded the command.
        /// </summary>
        [JsonPropertyName("backgroundedByUser")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? BackgroundedByUser { get; set; }

        /// <summary>
        /// Gets or sets the timeout value in milliseconds, set when the command hit its timeout and was auto-backgrounded.
        /// </summary>
        [JsonPropertyName("timedOutAfterMs")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? TimedOutAfterMs { get; set; }

        /// <summary>
        /// Gets or sets the model-facing note that the session working directory was not changed by a backgrounded command.
        /// </summary>
        [JsonPropertyName("backgroundCwdHint")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? BackgroundCwdHint { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this backgrounded command is terminated at its caller's final response.
        /// </summary>
        [JsonPropertyName("backgroundEndsWithFinalResponse")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? BackgroundEndsWithFinalResponse { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the subagent that owns this backgrounded command parks for its end and is woken by it.
        /// </summary>
        [JsonPropertyName("backgroundWakesOwner")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? BackgroundWakesOwner { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether sandbox mode was overridden.
        /// </summary>
        [JsonPropertyName("dangerouslyDisableSandbox")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? DangerouslyDisableSandbox { get; set; }

        /// <summary>
        /// Gets or sets the semantic interpretation for non-error exit codes with special meaning.
        /// </summary>
        [JsonPropertyName("returnCodeInterpretation")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ReturnCodeInterpretation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the command is expected to produce no output on success.
        /// </summary>
        [JsonPropertyName("noOutputExpected")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? NoOutputExpected { get; set; }

        /// <summary>
        /// Gets or sets the structured content blocks.
        /// </summary>
        [JsonPropertyName("structuredContent")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<JsonElement>? StructuredContent { get; set; }

        /// <summary>
        /// Gets or sets the path to the persisted full output, set when the output is too large for inline.
        /// </summary>
        [JsonPropertyName("persistedOutputPath")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PersistedOutputPath { get; set; }

        /// <summary>
        /// Gets or sets the total size of the output in bytes, set when the output is too large for inline.
        /// </summary>
        [JsonPropertyName("persistedOutputSize")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? PersistedOutputSize { get; set; }

        /// <summary>
        /// Gets or sets the model-facing reminder appended when a gh command reports a GitHub API rate-limit error.
        /// </summary>
        [JsonPropertyName("ghRateLimitHint")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? GhRateLimitHint { get; set; }

        /// <summary>
        /// Gets or sets the structured classification of git and GitHub operations detected in this command.
        /// </summary>
        [JsonPropertyName("gitOperation")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public GitOperationInfo? GitOperation { get; set; }
    }
}
