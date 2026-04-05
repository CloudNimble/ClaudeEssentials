using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Inputs
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

}
