using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools
{

    /// <summary>
    /// Represents the input parameters for the TodoWrite tool.
    /// The TodoWrite tool creates and manages a structured task list for the current coding session.
    /// </summary>
    public class TodoWriteToolInput
    {

        /// <summary>
        /// Gets or sets the updated todo list.
        /// </summary>
        [JsonPropertyName("todos")]
        public TodoItem[] Todos { get; set; } = [];

    }

    /// <summary>
    /// Represents a single todo item in the task list.
    /// </summary>
    public class TodoItem
    {

        /// <summary>
        /// Gets or sets the imperative form describing what needs to be done.
        /// </summary>
        /// <remarks>
        /// Example: "Run tests", "Build the project"
        /// </remarks>
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the present continuous form shown during execution.
        /// </summary>
        /// <remarks>
        /// Example: "Running tests", "Building the project"
        /// </remarks>
        [JsonPropertyName("activeForm")]
        public string ActiveForm { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status of the todo item.
        /// </summary>
        /// <remarks>
        /// Options: "pending", "in_progress", "completed"
        /// </remarks>
        [JsonPropertyName("status")]
        public string Status { get; set; } = "pending";

    }

}
