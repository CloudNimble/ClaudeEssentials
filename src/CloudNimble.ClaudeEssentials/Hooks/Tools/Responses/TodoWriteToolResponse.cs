using CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{
    /// <summary>
    /// Represents the response payload returned by the Claude Code TodoWrite tool after
    /// updating the task list.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The TodoWrite tool creates and manages a structured task list for the current coding
    /// session. It helps Claude track progress, organize complex tasks, and demonstrate
    /// thoroughness to the user. This response is received in the
    /// <see cref="TodoWritePostToolUsePayload"/> when the
    /// <c>tool_name</c> is "TodoWrite".
    /// </para>
    /// <para>
    /// The response includes both the previous state (<see cref="OldTodos"/>) and the new
    /// state (<see cref="NewTodos"/>) of the todo list, enabling hooks to track what changed.
    /// </para>
    /// <para>
    /// Use cases for the TodoWrite tool include:
    /// <list type="bullet">
    /// <item><description>Complex multi-step tasks requiring 3 or more distinct steps</description></item>
    /// <item><description>Tasks that require careful planning</description></item>
    /// <item><description>When the user provides multiple tasks to complete</description></item>
    /// <item><description>Tracking progress through large implementations</description></item>
    /// </list>
    /// </para>
    /// <para>
    /// Example JSON payload:
    /// <code>
    /// {
    ///   "oldTodos": [
    ///     { "content": "Run the build", "status": "completed", "activeForm": "Running the build" },
    ///     { "content": "Fix type errors", "status": "in_progress", "activeForm": "Fixing type errors" }
    ///   ],
    ///   "newTodos": [
    ///     { "content": "Run the build", "status": "completed", "activeForm": "Running the build" },
    ///     { "content": "Fix type errors", "status": "completed", "activeForm": "Fixing type errors" },
    ///     { "content": "Run tests", "status": "pending", "activeForm": "Running tests" }
    ///   ]
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class TodoWriteToolResponse
    {
        /// <summary>
        /// Gets or sets the previous state of the todo list before this update.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This contains the complete todo list as it existed before the TodoWrite
        /// operation was performed. Comparing this with <see cref="NewTodos"/> allows
        /// hooks to determine exactly what changed.
        /// </para>
        /// <para>
        /// This will be an empty list if no todos existed before this operation
        /// (i.e., this is the first TodoWrite in the session).
        /// </para>
        /// </remarks>
        [JsonPropertyName("oldTodos")]
        public List<TodoItem> OldTodos { get; set; } = [];

        /// <summary>
        /// Gets or sets the new state of the todo list after this update.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This contains the complete todo list after the TodoWrite operation.
        /// It reflects all additions, removals, and status changes that were made.
        /// </para>
        /// <para>
        /// Each <see cref="TodoItem"/> includes:
        /// <list type="bullet">
        /// <item><description><c>Content</c> - The task description (imperative form)</description></item>
        /// <item><description><c>Status</c> - Current state: pending, in_progress, or completed</description></item>
        /// <item><description><c>ActiveForm</c> - Present continuous form for display during execution</description></item>
        /// </list>
        /// </para>
        /// </remarks>
        [JsonPropertyName("newTodos")]
        public List<TodoItem> NewTodos { get; set; } = [];
    }
}
