using CloudNimble.ClaudeEssentials.Hooks;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
{

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
        /// See <see cref="TodoItemStatus"/> for possible values.
        /// </remarks>
        [JsonPropertyName("status")]
        public TodoItemStatus Status { get; set; } = TodoItemStatus.Pending;

    }

}
