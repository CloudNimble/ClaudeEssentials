using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CloudNimble.ClaudeEssentials.Hooks.Tools.Responses
{

    /// <summary>
    /// Represents the response payload returned by the TaskList tool.
    /// </summary>
    public class TaskListToolResponse
    {

        #region Properties

        /// <summary>
        /// Gets or sets the tasks in the list.
        /// </summary>
        [JsonPropertyName("tasks")]
        public List<TaskListItem> Tasks { get; set; } = [];

        #endregion

    }

}
